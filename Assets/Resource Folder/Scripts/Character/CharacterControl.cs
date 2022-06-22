using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Collect;
using _Game.Obstacle;
using OziLib;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour, ITouchPanel, CollectableDelegate
{
    [Header("Control")]
    [SerializeField] private TouchPanelController _touchPanelController;
    [SerializeField] private CharacterSO _characterSo;
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Text _stackCountText;

    [Header("Stack Fields")]
    [SerializeField] private List<Transform> _stackList;
    [SerializeField] private Transform _stackInitPos;
    [SerializeField] private Transform _newStackPos;
    [SerializeField] private GameObject _diamondObject;
    private int _maxStackCount;
    private int _currentStackCount;
    private Vector3 _initPos;
    private Vector3 _targetPos;
    private float _refVel;

    private bool _isRunStart;

    private const string IdleName = "Idle";
    private const string RunName = "Run";
    private const string StackRunName = "Run2";
    private const string DanceName = "Dance";
    
    #region UNITY_METHODS

    private void OnEnable()
    {
        EventManager.StartListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StartListening(EventTags.LEVEL_END, LevelEnd);
        EventManager.StartListening(EventTags.STACK_COUNT_BUTTON, StackCountButton);
        EventManager.StartListening(EventTags.DIA_DESTROY, DiaDestroy);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StopListening(EventTags.LEVEL_END, LevelEnd);
        EventManager.StopListening(EventTags.STACK_COUNT_BUTTON, StackCountButton);
        EventManager.StopListening(EventTags.DIA_DESTROY, DiaDestroy);
    }
    
    void Start()
    {
        _stackList = new List<Transform>();
        _touchPanelController ??= FindObjectOfType<TouchPanelController>();
        _touchPanelController.InitPanel(this);
        _initPos = transform.position;
        _maxStackCount = DataManager.instance.GetIntData(EventTags.STACK_MAX_COUNTER);
        var diamondCreateCount = DataManager.instance.GetIntData(EventTags.START_STACK_COUNT);
        StackCountUIUpdate();
        for (int i = 0; i < diamondCreateCount; i++)
        {
            DiamondCreater();
        }
    }
    
    void Update()
    {
        if(!_isRunStart) return;
        // Vertical Movement
        var position = transform.position;
        position += Vector3.forward * _characterSo.VerticalSpeed * Time.deltaTime;

        // Horizontal Movement
        position.x = Mathf.SmoothDamp(transform.position.x, _targetPos.x, ref _refVel, 0.07f);
        transform.position = position;

        if (_stackList.Count != 0)
        {
            _stackList[0].position = _stackInitPos.position;
        }
        
        if(_stackList.Count <= 1) return;
        for (int i = 1; i < _stackList.Count; i++)
        {
            var currentPosition = _stackList[i].position;
            var targetPosition = _stackList[i - 1].position;
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, _characterSo.SmoothTime * Time.deltaTime);
            _stackList[i].position = currentPosition;
        }
    }

    #endregion

    #region METHODS

    private void DiamondCollect(ICollectable collectable,  Action<bool> callback = null)
    {
        if (_maxStackCount <= _currentStackCount)
        {
            callback?.Invoke(false);
            return;
        }
        callback?.Invoke(true);
        _currentStackCount++;
        StackCountAdd();
        StackFollowInit(collectable);
    }

    private void CoinCollect(ICollectable collectable)
    {
        EventManager.TriggerEvent(EventTags.COIN_COLLECT, this);
    }

    private void ObstacleBladeAction(IObstacle obstacle)
    {
        print("Obstacle tigger");
    }

    private void StackCountAdd()
    {
        StackCountUIUpdate();
        if(!_isRunStart) return;
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(StackRunName))
        {
            _animator.CrossFadeInFixedTime(StackRunName, 0f);
        }
    }

    private void StackFollowInit(ICollectable collectable)
    {
        var collectParent = collectable.getGameObject;
        collectParent.transform.position = _newStackPos.position;
        collectable.getCollectItem.localPosition = Vector3.up *.5f;
        StartCoroutine(ScaleLerp(collectable.getCollectItem, Vector3.one, .2f, 0f,_characterSo.Curve));
        _stackList.Add(collectParent.transform);
        _newStackPos.position += Vector3.back;
    }

    private void StackCountUIUpdate()
    {
        var fillValue = ((float)_currentStackCount / _maxStackCount);
        _fillImage.fillAmount = fillValue;
        _stackCountText.text = _currentStackCount + "/" + _maxStackCount;
    }

    private void DiamondCreater()
    {
        var diaObject = Instantiate(_diamondObject);
        // //diaObject.transform.GetChild(1).gameObject.SetActive(false);
        // StackFollowInit(diaObject.GetComponent<ICollectable>());
    }

    #endregion

    #region INTERFACES

    public void OnPointDownAction(Vector2 delta)
    {
        _targetPos = transform.position;
    }

    public void OnDragAction(Vector2 delta)
    {
        _targetPos.x += delta.x * _characterSo.HorizontalSensitivity;
        _targetPos.x = Mathf.Clamp(_targetPos.x, _initPos.x - _characterSo.PlayerHorizontalClamp, _initPos.x + _characterSo.PlayerHorizontalClamp);
    }
    
    public void onItemCollect(ICollectable collectable, Action<bool> callback = null)
    {
        switch (collectable.getCollectableType)
        {
            case CollectableType.Diamond:
                DiamondCollect(collectable, callback);
                break;
            case CollectableType.Coin:
                CoinCollect(collectable);
                callback?.Invoke(true);
                break;
        }
        
    }

    public void onObstacleInteract(IObstacle obstacle)
    {
        switch (obstacle.getObstacleType)
        {
            case ObstacleType.CircularBlade:
                ObstacleBladeAction(obstacle);
                break;
        }
    }
    
    #endregion

    #region ACTION
    
    private void LevelStart(object arg0)
    {
        _isRunStart = true;
        _animator.CrossFadeInFixedTime(_currentStackCount == 0 ? RunName : StackRunName, .2f);
    }

    private void LevelEnd(object arg0)
    {
        _isRunStart = false;
        _animator.CrossFadeInFixedTime(DanceName, .2f);
    }
    
    private void StackCountButton(object arg0)
    {
        StackCountAdd();
        DiamondCreater();
    }
    
    private void DiaDestroy(object arg0)
    {
        if (arg0 is GameObject diaObject)
        {
            var index = _stackList.FindIndex(x => x.Equals(diaObject.transform));
            var range = _stackList.Count - index;
            _stackList.RemoveRange(index,range);
           _currentStackCount -= range;
           if (_currentStackCount == 0)
           {
               _animator.CrossFadeInFixedTime(RunName, .2f);
           }
           StackCountUIUpdate();
        }
            
    }

    #endregion

    #region HELPER

    IEnumerator ScaleLerp(Transform moveObject, Vector3 endValue, float duration,float delayTime = 0f, AnimationCurve curve = null,System.Action<object> callback = null)
    {
        yield return new WaitForSeconds(delayTime);
        float time = 0;
        Vector3 startScale = moveObject.localScale;
        while (time < duration)
        {
            var scaleModifier = Mathf.Lerp(startScale.x, endValue.x, curve.Evaluate(time) / duration );
            moveObject.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }
        moveObject.transform.localScale = endValue;
        callback?.Invoke(this);
    } 

    #endregion
}

    



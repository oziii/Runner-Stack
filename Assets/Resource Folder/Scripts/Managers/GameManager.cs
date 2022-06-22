using System;
using System.Collections;
using System.Collections.Generic;
using OziLib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelSO[] _levelArr;
    [SerializeField] private UIManager _uiManager;
    private Level _currentLevel;
    #region UNITY_METHODS

    private void OnEnable()
    {
        EventManager.StartListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StartListening(EventTags.LEVEL_END, LevelEnd);
        EventManager.StartListening(EventTags.NEXT_LEVEL, onNextLevel);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventTags.LEVEL_START, LevelStart);
        EventManager.StopListening(EventTags.LEVEL_END, LevelEnd);
        EventManager.StopListening(EventTags.NEXT_LEVEL, onNextLevel);
    }

    private void Start()
    {
        LevelCreater();
    }

    #endregion

    #region METHODS

    private void LevelCreater()
    {
        var levelCount = DataManager.instance.GetIntData(EventTags.LEVEL_COUNTER);
        var level = _levelArr[levelCount % _levelArr.Length].LevelPrefab;
        _currentLevel = Instantiate(level);
        _uiManager.init();
    }

    #endregion
    
    #region ACTION

    private void LevelEnd(object arg0)
    {
        var levelCount = DataManager.instance.GetIntData(EventTags.LEVEL_COUNTER);
        levelCount++;
        DataManager.instance.SetIntData(EventTags.LEVEL_COUNTER, levelCount);
        
    }

    private void LevelStart(object arg0)
    {
        
    }
    
    private void onNextLevel(object arg0)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion
}

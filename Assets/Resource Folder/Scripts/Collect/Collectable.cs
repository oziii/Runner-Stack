using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Collect
{
    public interface ICollectable
    {
        CollectableType getCollectableType { get; }
        GameObject getGameObject { get; }
        
        Transform getCollectItem { get; }
        void setCollectState(bool isState);
    }

    public interface CollectableDelegate
    {
        void onItemCollect(ICollectable collectable, System.Action<bool> callback = null);
    }

    public class Collectable : MonoBehaviour, ICollectable
    {
        [Header("Collectable Fields")] [SerializeField]
        private CollectableType _collectableType;

        [SerializeField] private bool _collectItemTurn;
        [SerializeField] float _rotationSpeed = 180;
        [SerializeField] float _verticalMovementSpeed = 10;
        [SerializeField] float _verticalMovementFactor = .5f;
        [SerializeField] float _factor = .1f;
        [SerializeField] protected Transform _collectModel;
        [SerializeField] ParticleSystem _collectedVFX;
        protected bool _isCollect;
        private Quaternion _startQuaternion;

        #region UNITY METHODS
        virtual protected void Start()
        {
            if (!_collectModel) return;
            _collectModel = transform.GetChild(0);
            _startQuaternion = _collectModel.localRotation;
        }

        virtual protected void Update()
        {
            if (_isCollect) return;
            if (!_collectItemTurn) return;
            _collectModel.Rotate(Vector3.up, Time.deltaTime * _rotationSpeed);
            var localPos = _collectModel.localPosition;
            localPos.y = (Mathf.Sin(Time.time * _verticalMovementSpeed + transform.position.z * _factor) * .5f + .5f) *
                         _verticalMovementFactor;
            _collectModel.localPosition = localPos;
        }

        #endregion

        #region OVERRIDED

        virtual protected void onTriggerCompleted()
        {
        }

        #endregion

        #region METHOD

        #endregion

        #region INTERFACE

        CollectableType ICollectable.getCollectableType => _collectableType;

        GameObject ICollectable.getGameObject => gameObject;

        Transform ICollectable.getCollectItem => _collectModel;

        public void setCollectState(bool isState)
        {
            _isCollect = isState;
        }

        #endregion

        #region ACTION

        virtual protected void OnTriggerEnter(Collider other)
        {
            if (_isCollect) return;

            if (other.GetComponent<CollectableDelegate>() is CollectableDelegate collectableDelegate)
            {
                _isCollect = true;
                
                collectableDelegate.onItemCollect(this, b =>
                {
                    if (b)
                    {

                        onTriggerCompleted();
                        _collectModel.localRotation = _startQuaternion;
                        _collectModel.localScale = Vector3.one * 0.01f;
                        if (_collectedVFX)
                        {
                            _collectedVFX.gameObject.SetActive(true);
                            _collectedVFX.Play();
                        }
                    }
                    else
                    {
                        _isCollect = false;
                    }
                });
            }
        }

        #endregion

        #region HELPER

        #endregion
    }

    public enum CollectableType
    {
        Diamond,
        Coin
    }
}

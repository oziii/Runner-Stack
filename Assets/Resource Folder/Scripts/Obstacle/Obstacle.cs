using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Obstacle
{
    public interface IObstacle
    {
        ObstacleType getObstacleType { get; }
        float getDamageValue { get; }

        void setObstacleState(bool isState);

        void setDirection(Vector3 dir);
    }

    public interface ObstacleDelegate
    {
        void onObstacleInteract(IObstacle obstacle);
    }

    public class Obstacle : MonoBehaviour, IObstacle
    {
        [Header("Obstacle Fields")]
        [SerializeField] private ObstacleType _obstacleType;
        [SerializeField] private float _damageValue;
        [Header("Turn Fields")]
        [SerializeField] protected bool _obstacleItemTurn;
        [SerializeField] float _rotationSpeed = 180;
        [Header("Visual Fields")] 
        [SerializeField] private Transform _obstacleModel;
        [SerializeField] ParticleSystem _interactVFX;
        protected bool _isInteract;
        protected bool _isObstacleActive;
        private Vector3 _direction;
        #region UNITY METHODS
        
        virtual protected void Start()
        {
            if (!_obstacleModel)
                _obstacleModel = transform.GetChild(0);
            setObstacleState(true);

        }

        virtual protected void Update()
        {
            if(_isInteract) return;
            if(!_obstacleItemTurn) return;
            _obstacleModel.Rotate(Vector3.forward, Time.deltaTime * _rotationSpeed);

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

        public ObstacleType getObstacleType => _obstacleType;

        public float getDamageValue => _damageValue;


        public void setObstacleState(bool isState)
        {
            _isObstacleActive = isState;
        }

        public void setDirection(Vector3 dir)
        {
            _direction = dir;
        }

        #endregion

        #region ACTION

        virtual protected void OnTriggerEnter(Collider other)
        {
            if(!_isObstacleActive) return;
            if (other.GetComponent<ObstacleDelegate>() is ObstacleDelegate obstacleDelegate)
            {
                _isInteract = true;

                if (_obstacleModel)
                {
                    obstacleDelegate.onObstacleInteract(this);
                }


                if (_interactVFX)
                {
                    _interactVFX.Play();    
                }

                setObstacleState(false);
                onTriggerCompleted();
            }
        }
        
        
        
        #endregion

        #region HELPER

        #endregion

    }

    public enum ObstacleType
    {
        CircularBlade,
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrolObject : MonoBehaviour
{
    [Header("Patrol Fields")]
    [SerializeField] protected bool _isObjectPatrol;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private List<Transform> _targetPositions;
    [SerializeField] private Transform _patrolObject;
    private int _positionIndex;
    private Vector3 _targetPosition;
    private void Start()
    {
        if(!_isObjectPatrol) return;
        SetTarget(0);
    }

    private void Update()
    {
        if(!_isObjectPatrol) return;
        _patrolObject.position = Vector3.MoveTowards(_patrolObject.position, _targetPosition, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(_patrolObject.position, _targetPosition) < 0.01f)
        {
            SetTarget(_positionIndex+1);
        }
    }
    
    private void SetTarget(int index)
    {
        _positionIndex = index;
        _targetPosition = _targetPositions[_positionIndex % _targetPositions.Count].position;
    }
}

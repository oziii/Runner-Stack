using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraOffsetSO", menuName = "SO/CameraOffsetSO")]
public class CameraOffsetSO : ScriptableObject
{
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;
    [SerializeField] private bool _autoOffset;
    
    public bool AutoOffset => _autoOffset;

    public Vector3 PositionOffset
    {
        get => _positionOffset;
        set => _positionOffset = value;
    }

    public Vector3 RotationOffset
    {
        get => _rotationOffset;
        set => _rotationOffset = value;
    }
} 

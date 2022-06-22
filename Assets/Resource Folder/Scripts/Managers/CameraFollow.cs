using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _target;
    [SerializeField] private CameraOffsetSO _cameraOffsetSo;
    [SerializeField, ReadOnly] private Vector3 _positionOffset;
    [SerializeField, ReadOnly] private Vector3 _rotationOffset;

    public LayerMask layerMask;
    private Camera _camera;

    #endregion

    #region Unity_Methods

    private void Start()
    {
        _camera = GetComponent<Camera>();
        if (_cameraOffsetSo.AutoOffset)
        {
            _positionOffset = transform.position - _target.position;
            _rotationOffset = transform.eulerAngles - _target.eulerAngles;
            _cameraOffsetSo.PositionOffset = _positionOffset;
            _cameraOffsetSo.RotationOffset = _rotationOffset;
        }

    }

    void LateUpdate()
    {
        CamMovementFollow();
        CamRotationFollow();
    }
    #endregion

    #region CameraFollow_Methods

    private void CamMovementFollow()
    {
        transform.position = _target.position + _target.rotation * _cameraOffsetSo.PositionOffset;
    }

    private void CamMovementFollow(float lerpTime)
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _cameraOffsetSo.PositionOffset, Time.deltaTime * lerpTime);
    }

    private void CamRotationFollow()
    {
        transform.rotation = _target.rotation * Quaternion.Euler(_cameraOffsetSo.RotationOffset);
    }

    private void CamRotationFollow(float lerpTime)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), Time.deltaTime * lerpTime);
    }
    
    private void ObstacleControl(GameObject targetObject)
    {
        RaycastHit hit;
        if (Physics.Linecast(targetObject.transform.position, transform.position, out hit, layerMask))
        {
            transform.position = hit.point + (targetObject.transform.position - _cameraOffsetSo.PositionOffset).normalized * (_camera.nearClipPlane + 0.1f);
        }
    }
    
    #endregion

}

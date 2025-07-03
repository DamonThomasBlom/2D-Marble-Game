using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTransformLerp : MonoBehaviour
{
    public Transform referenceTransform;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float lerpDuration = 1.0f;
    public LeanTweenType moveType;
    public LeanTweenType rotateType;

    private Transform _ObjectToMove;
    private Vector3 _startPosition;
    private Vector3 _startRotation;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _startRotation = transform.localEulerAngles;

        Move();
    }

    void AssignVariables()
    {
        if (referenceTransform != null)
            _ObjectToMove = referenceTransform;
        else
            _ObjectToMove = this.transform;
    }

    [Button]
    public void Move()
    {
        if (!enabled) { return; }
        AssignVariables();

        transform.localPosition = _startPosition;
        transform.localEulerAngles = _startRotation;

        // Lerping local position
        LeanTween.moveLocal(_ObjectToMove.gameObject, targetPosition, lerpDuration)
            .setOnUpdate((Vector3 val) => _ObjectToMove.localPosition = val)
            .setEase(moveType)
            .setOnComplete(_OnLerpComplete);

        // Lerping local rotation
        LeanTween.rotateLocal(_ObjectToMove.gameObject, targetRotation, lerpDuration)
            .setOnUpdate((Vector3 val) => _ObjectToMove.localEulerAngles = val)
            .setEase(moveType);
    }

    [Button]
    public void CopyCurrentTransform()
    {
        if (referenceTransform != null)
        {
            targetPosition = referenceTransform.localPosition;
            targetRotation = referenceTransform.localEulerAngles;
        }
        else
        {
            targetPosition = transform.localPosition;
            targetRotation = transform.localEulerAngles;
        }
    }

    private void _OnLerpComplete()
    {
        Move();
    }
}

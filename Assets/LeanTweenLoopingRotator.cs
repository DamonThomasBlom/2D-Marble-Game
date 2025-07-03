using UnityEngine;

public class LeanTweenLoopingRotator : MonoBehaviour
{
    [Header("Transform Targets")]
    public Transform targetTransform; // Optional, if you want to move or rotate toward a Transform
    public Vector3 targetRotationEuler = new Vector3(0, 180, 0);
    public Vector3 startRotationEuler = Vector3.zero;
    public float duration = 2f;

    [Header("Optional Position Looping")]
    public bool loopPosition = false;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    void Start()
    {
        // Set start rotation
        transform.localRotation = Quaternion.Euler(startRotationEuler);

        // Start rotation loop
        LeanTween.rotateLocal(gameObject, targetRotationEuler, duration)
                 .setEaseInOutSine()
                 .setLoopPingPong();

        // Optional position loop
        if (loopPosition)
        {
            transform.localPosition = startPosition;

            LeanTween.moveLocal(gameObject, targetPosition, duration)
                     .setEaseInOutSine()
                     .setLoopPingPong();
        }
    }
}

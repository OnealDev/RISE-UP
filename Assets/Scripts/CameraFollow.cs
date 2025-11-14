using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [HideInInspector] 
    public bool allowFollow = true;   

    void LateUpdate()
    {
        if (!allowFollow) return;      //Stops camera from taking over shake
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}

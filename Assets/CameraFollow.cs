using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     [Header("Target")]
     public Transform target; // Player (Priest)

     [Header("Camera Settings")]
     public float smoothSpeed = 0.125f;
     public Vector3 offset;

     [Header("Bounds")]
     public Vector2 minPosition;
     public Vector2 maxPosition;

     private Camera cam;

     void Start()
     {
          cam = Camera.main;
     }

     void LateUpdate()
     {
          if (target == null) return;

          // Desired camera position
          Vector3 desiredPosition = target.position + offset;

          // Smoothly interpolate between current and desired
          Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

          // Keep Z the same
          smoothedPosition.z = transform.position.z;

          // Camera extents (so it doesn’t show outside map)
          float vertExtent = cam.orthographicSize;
          float horzExtent = vertExtent * Screen.width / Screen.height;

          // Clamp AFTER smoothing
          float clampX = Mathf.Clamp(smoothedPosition.x, minPosition.x + horzExtent, maxPosition.x - horzExtent);
          float clampY = Mathf.Clamp(smoothedPosition.y, minPosition.y + vertExtent, maxPosition.y - vertExtent);

          Vector3 finalPosition = new Vector3(clampX, clampY, smoothedPosition.z);

          // Only move if the difference is significant (prevents jitter)
          if ((finalPosition - transform.position).sqrMagnitude > 0.0001f)
          {
               transform.position = finalPosition;
          }
     }
}


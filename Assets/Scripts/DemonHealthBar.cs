using UnityEngine;

public class DemonHealthBarFollow : MonoBehaviour
{
     [Header("Target Settings")]
     public Transform demon; // Assign the demon GameObject in Inspector
     public Vector3 offset = new Vector3(0, 1.5f, 0); // Adjust Y to float above head

     [Header("Facing Settings")]
     public bool faceCamera = true; // Optional: make it always face the camera

     private Camera mainCam;

     void Start()
     {
          mainCam = Camera.main;
     }

     void LateUpdate()
     {
          if (demon == null) return;

          // Position health bar above demon
          transform.position = demon.position + offset;

          // Optional: rotate to face camera
          if (faceCamera && mainCam != null)
          {
               transform.forward = mainCam.transform.forward;
          }
     }
}

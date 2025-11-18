using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFallEffect : MonoBehaviour
{
     [Header("Fall Settings")]
     public Vector3 landingPoint = new Vector3(0, 0, 0);
     public float fallDuration = 3f;
     public AnimationCurve fallCurve; // Assign in Inspector for easing
     public bool lockMovementDuringFall = true;

     [Header("Landing Effects")]
     public GameObject landingEffect; // Particle prefab
     public AudioClip landingSound; // Optional sound
     public float cameraShakeDuration = 0.3f;
     public float cameraShakeMagnitude = 0.2f;

     private Rigidbody2D rb;
     private AudioSource audioSource;
     private bool isFalling = true;

     
     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          audioSource = GetComponent<AudioSource>();
          StartCoroutine(FallToLanding());
     }

     IEnumerator FallToLanding()
     {
          Vector3 startPos = transform.position;
          float elapsed = 0f;

          while (elapsed < fallDuration)
          {
               float t = elapsed / fallDuration;
               t = fallCurve != null ? fallCurve.Evaluate(t) : t;
               rb.MovePosition(Vector3.Lerp(startPos, landingPoint, t));
               elapsed += Time.deltaTime;
               yield return null;
          }

          rb.MovePosition(landingPoint);
          isFalling = false;

          // Trigger landing effects
          if (landingEffect != null)
               Instantiate(landingEffect, landingPoint, Quaternion.identity);

          if (landingSound != null && audioSource != null)
               audioSource.PlayOneShot(landingSound);

          StartCoroutine(CameraShake(cameraShakeDuration, cameraShakeMagnitude));

          Debug.Log("Player landed. Game starts!");
     }

     IEnumerator CameraShake(float duration, float magnitude)
     {
          Transform cam = Camera.main.transform;
          Vector3 originalPos = cam.localPosition;
          float elapsed = 0f;

          while (elapsed < duration)
          {
               float x = Random.Range(-1f, 1f) * magnitude;
               float y = Random.Range(-1f, 1f) * magnitude;
               cam.localPosition = new Vector3(x, y, originalPos.z);
               elapsed += Time.deltaTime;
               yield return null;
          }

          cam.localPosition = originalPos;
     }

     public bool IsFalling()
     {
          return isFalling;
     }
}
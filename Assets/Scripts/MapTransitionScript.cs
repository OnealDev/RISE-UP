using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapTransitionScript : MonoBehaviour
{
     [Header("Teleport Settings")]
     public Transform teleportTarget;
     public GameObject cameraObject;

     [Header("Fade Settings")]
     public CanvasGroup fadePanel;   // Assign your FadePanel here
     public float fadeDuration = 1f; // Duration of fade in seconds

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               StartCoroutine(FadeTransition(collision.transform));
          }
     }

     private IEnumerator FadeTransition(Transform player)
     {
          // Fade out
          yield return StartCoroutine(Fade(1));

          // Teleport player
          player.position = teleportTarget.position;

          // Move camera to match new player position
          if (cameraObject != null)
          {
               CameraFollow camFollow = cameraObject.GetComponent<CameraFollow>();
               if (camFollow != null)
               {
                    Vector3 newCamPos = teleportTarget.position + camFollow.offset;
                    newCamPos.z = cameraObject.transform.position.z;
                    cameraObject.transform.position = newCamPos;
               }
          }

          // Fade back in
          yield return StartCoroutine(Fade(0));
     }

     private IEnumerator Fade(float targetAlpha)
     {
          float startAlpha = fadePanel.alpha;
          float time = 0f;

          while (time < fadeDuration)
          {
               time += Time.deltaTime;
               fadePanel.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
               yield return null;
          }

          fadePanel.alpha = targetAlpha;
     }
}

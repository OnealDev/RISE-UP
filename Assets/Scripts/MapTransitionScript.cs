using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapTransitionScript : MonoBehaviour
{
     [Header("Requirements")]
     public bool requiresKills = false;
     public int requiredKillCount = 0;
     public string blockedMessage = "You must defeat more Lava Slimes!";
     public Text messageText;  // Optional UI popup

     [Header("Teleport Settings")]
     public Transform teleportTarget;
     public GameObject cameraObject;

     [Header("Fade Settings")]
     public CanvasGroup fadePanel;   // Assign your FadePanel here
     public float fadeDuration = 1f; // Duration of fade in seconds

     [Header("Environment Layers")]
     [Tooltip("Set to 'Cave' if this trigger leads into the cave, or 'Outside' for outside world.")]
     public string targetSortingLayer = "Outside";  // You can change per trigger in Inspector

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (!collision.CompareTag("Player"))
               return;

          // If this teleport requires slime kills…
          if (requiresKills)
          {
               if (!EnemyKillTracker.Instance.HasMetRequirement())
               {
                    // Not enough kills: Block access
                    Debug.Log(blockedMessage);

                    if (messageText != null)
                    {
                         messageText.text = blockedMessage;
                         StartCoroutine(ClearMessage());
                    }

                    return;
               }
          }

          // Requirement met: Proceed with teleport
          StartCoroutine(FadeTransition(collision.transform));
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

          // Change lighting/sorting layer
          SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
          if (playerSprite != null)
          {
               playerSprite.sortingLayerName = targetSortingLayer;
               Debug.Log("Switched player sorting layer to: " + targetSortingLayer);
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

     private IEnumerator ClearMessage()
     {
          yield return new WaitForSeconds(2f);
          messageText.text = "";
     }

}


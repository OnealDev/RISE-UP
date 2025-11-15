using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour, IInteractable
{
     [Header("Gate Setup")]
     public Animator animator;

     [Header("Shake Settings")]
     public float shakeDuration = 0.3f;
     public float shakeMagnitude = 0.2f;

     private bool isOpened = false;
     private Collider2D gateCollider;

     private void Awake()
     {
          // Cache the collider so we can disable it when the gate opens
          gateCollider = GetComponent<Collider2D>();
     }

     public bool CanInteract()
     {
          return !isOpened;
     }

     public void Interact()
     {
          if (isOpened) return;

          PlayerInventory inv = Object.FindAnyObjectByType<PlayerInventory>();
          if (inv != null && inv.keyCount > 0)
          {
               inv.keyCount--;
               isOpened = true;

               // Play the open animation
               animator.SetTrigger("Open");

               // Shake the camera for feedback
               StartCoroutine(ScreenShake());

               // Disable collider so player can walk through
               if (gateCollider != null)
                    gateCollider.enabled = false;

               Debug.Log("Gate opened!");
          }
          else
          {
               Debug.Log("Gate requires a key.");
          }
     }

     private IEnumerator ScreenShake()
     {
          Vector3 originalPos = Camera.main.transform.position;

          float elapsed = 0f;
          while (elapsed < shakeDuration)
          {
               float x = Random.Range(-1f, 1f) * shakeMagnitude;
               float y = Random.Range(-1f, 1f) * shakeMagnitude;

               Camera.main.transform.position = originalPos + new Vector3(x, y, 0);

               elapsed += Time.deltaTime;
               yield return null;
          }

          Camera.main.transform.position = originalPos;
     }
}


using System.Collections;
using UnityEngine;

public class ArchangelNPC : MonoBehaviour
{
     [Header("Movement Settings")]
     public float moveSpeed = 3f;
     public float interactionDistance = 1f;
     public LayerMask obstacleLayerMask;

     [Header("Dialogue Settings")]
     public string dialogueText = "Find the path to Rise UP.";
     public float dialogueDuration = 2f;
     public float disappearDelay = 1f;

     private Transform player;
     private bool isInteracting = false;
     private Animator animator;
     private Collider2D npcCollider;
     private Rigidbody2D rb;

     void Awake()
     {
          animator = GetComponent<Animator>();
          npcCollider = GetComponent<Collider2D>();
          rb = GetComponent<Rigidbody2D>();
     }

     void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player") && !isInteracting)
          {
               player = other.transform;
               StartCoroutine(ApproachAndInteract());
          }
     }

     IEnumerator ApproachAndInteract()
     {
          isInteracting = true;

          // Freeze player movement
          AryasPlayerMovement playerMovement = player.GetComponent<AryasPlayerMovement>();
          if (playerMovement != null)
               playerMovement.enabled = false;

          while (Vector2.Distance(transform.position, player.position) > interactionDistance)
          {
               Vector2 direction = (player.position - transform.position).normalized;

               // Wall detection
               RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, obstacleLayerMask);
               if (hit.collider != null)
               {
                    Debug.Log("NPC blocked by: " + hit.collider.name);
                    break; // Stop moving if blocked
               }

               rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

               if (animator != null)
                    animator.SetBool("IsWalking", true);

               yield return new WaitForFixedUpdate();
          }

          if (animator != null)
               animator.SetBool("IsWalking", false);

          // Show dialogue
          AngelDialogue angelDialogue = Object.FindAnyObjectByType<AngelDialogue>();
          if (angelDialogue != null)
               angelDialogue.ShowDialogue(dialogueText);

          yield return new WaitForSeconds(dialogueDuration + disappearDelay);

          // Unfreeze player
          if (playerMovement != null)
               playerMovement.enabled = true;

          // Disable collider and destroy NPC
          if (npcCollider != null)
               npcCollider.enabled = false;

          Destroy(gameObject);
     }
}


using UnityEngine;

public class TalkPrompt : MonoBehaviour
{
     public Canvas promptCanvas;     // World-space canvas above NPC
     public KeyCode key = KeyCode.E;

     private bool playerInRange = false;
     private ArchangelNPC npc;       // Your existing NPC script

     void Start()
     {
          npc = GetComponent<ArchangelNPC>();
          promptCanvas.gameObject.SetActive(false);
     }

     void Update()
     {
          if (!playerInRange)
          {
               promptCanvas.gameObject.SetActive(false);
               return;
          }

          // Show the prompt (always, even after dialogue is over)
          promptCanvas.gameObject.SetActive(true);

          // Press E to interact
          if (Input.GetKeyDown(key))
          {
               npc.Interact();
          }
     }

     void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
               playerInRange = true;
     }

     void OnTriggerExit2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
               playerInRange = false;
     }
}

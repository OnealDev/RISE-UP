using UnityEngine;

public class NPCTalkPrompt : MonoBehaviour
{
     public GameObject talkPromptUI;   // Assign the "Press E to Talk" UI
     private bool playerInRange = false;
     private ArchangelNPC npc;          // Your existing NPC script

     void Start()
     {
          talkPromptUI.SetActive(false);
          npc = GetComponent<ArchangelNPC>();
     }

     void Update()
     {
          // Show prompt when close
          if (playerInRange)
          {
               talkPromptUI.SetActive(true);

               // If player presses E
               if (Input.GetKeyDown(KeyCode.E))
               {
                    npc.Interact();  // Calls the dialogue
                    talkPromptUI.SetActive(false); // Hide prompt during dialogue
               }
          }
          else
          {
               talkPromptUI.SetActive(false);
          }
     }

     void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player") && npc.CanInteract())
          {
               playerInRange = true;
          }
     }

     void OnTriggerExit2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               playerInRange = false;
          }
     }
}

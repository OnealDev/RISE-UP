using UnityEngine;
using System.Collections.Generic;

public class ArchangelNPC : MonoBehaviour, IInteractable
{
     [Header("Dialogue Settings")]
     public List<string> dialogueLines = new List<string>
    {
        "Greetings, traveler.",
        "You must find the path to Rise UP.",
        "The light will guide you."
    };

     private bool hasInteracted = false;

     public bool CanInteract() => true;


     public void Interact()
     {
          if (hasInteracted) return;
          

          AngelDialogue angelDialogue = Object.FindFirstObjectByType<AngelDialogue>();
          if (angelDialogue != null)
          {
               angelDialogue.ShowDialogue(dialogueLines);
          }
     }
}
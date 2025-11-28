using UnityEngine;

public class SkeletonInteractable : MonoBehaviour, IInteractable
{
     [TextArea(2, 4)]
     public string skeletonDialogue = "I have been waiting for someone like you...";

     private bool canTalk = true;

     public bool CanInteract()
     {
          return canTalk; // allow interaction
     }

     public void Interact()
     {
          Debug.Log("Skeleton is talking to the player!");

          // Your dialogue UI appears here
          DialogueManager.Instance.ShowDialogue(skeletonDialogue);

          // Optional: prevent spamming
          // canTalk = false;
     }
}

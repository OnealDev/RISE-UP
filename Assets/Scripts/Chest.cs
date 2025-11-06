using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
     public bool IsOpened { get; private set; }

     [Header("Chest Setup")]
     public Animator animator;
     public GameObject itemInside; // Bible inside the chest

     void Start()
     {
          if (itemInside != null)
               itemInside.SetActive(false);
     }

     public bool CanInteract()
     {
          return !IsOpened;
     }

     public void Interact()
     {
          if (!CanInteract()) return;

          IsOpened = true;
          animator.SetTrigger("Open"); // triggers animation
     }

     // Called at the END of your chest-open animation
     public void ReleaseItem()
     {
          if (itemInside != null)
          {
               itemInside.SetActive(true);

               // Let it bounce if it has a BounceEffect
               BounceEffect bounce = itemInside.GetComponent<BounceEffect>();
               if (bounce != null)
                    bounce.StartBounce();
          }
     }
}

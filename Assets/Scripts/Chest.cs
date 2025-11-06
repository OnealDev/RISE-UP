using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
     public bool IsOpened { get; private set; }

     [Header("Chest Setup")]
     public Animator animator;
     public GameObject itemInside; // Bible inside the chest
     public GameObject keyPrefab;  // NEW — optional key drop

     [Header("Item Drop Settings")]
     [Tooltip("How far below the chest the item should appear.")]
     public float spitDistance = 0.5f;
     [Tooltip("How strongly the item is pushed out of the chest.")]
     public float spitForce = 5f;

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
          animator.SetTrigger("Open"); // triggers chest animation
     }

     // Called at the END of your chest-open animation
     public void ReleaseItem()
     {
          // Drop the Bible if assigned
          if (itemInside != null)
          {
               itemInside.SetActive(true);
               DropObject(itemInside);
          }

          // Drop a Key if assigned
          if (keyPrefab != null)
          {
               GameObject key = Instantiate(keyPrefab);
               DropObject(key);
          }
     }

     private void DropObject(GameObject obj)
     {
          // Always spit the item downward in world space
          Vector2 forward = Vector2.down;
          Vector2 spawnPosition = (Vector2)transform.position + forward * spitDistance;

          // Move the item slightly downward before applying force
          obj.transform.position = spawnPosition;

          // Add a small push to "spit it out" downward
          Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
          if (rb != null)
          {
               rb.AddForce(forward * spitForce, ForceMode2D.Impulse);
          }

          // Trigger bounce if available
          BounceEffect bounce = obj.GetComponent<BounceEffect>();
          if (bounce != null)
               bounce.StartBounce();
     }
}

using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
     [Header("Chest Setup")]
     public Animator animator;            // Animator for open animation
     public GameObject itemInside;        // Bible or other item inside chest
     public GameObject keyPrefab;         // Optional key to spawn

     [Header("Item Drop Settings")]
     [Tooltip("How far below the chest the item should appear.")]
     public float spitDistance = 0.5f;
     [Tooltip("How strongly the item is pushed out of the chest.")]
     public float spitForce = 5f;

     private bool isOpened = false;

     private void Start()
     {
          // Hide item inside chest until opened
          if (itemInside != null)
               itemInside.SetActive(false);
     }

     public bool CanInteract()
     {
          return !isOpened;
     }

     public void Interact()
     {
          if (isOpened) return;

          isOpened = true;
          if (animator != null)
               animator.SetTrigger("Open"); // Triggers animation
          else
               ReleaseItem(); // Fallback if no animation
     }

     // Called by animation event OR after Interact() if no animation
     public void ReleaseItem()
     {
          // Drop the internal item (Bible, potion, etc.)
          if (itemInside != null)
          {
               if (!itemInside.activeSelf)
                    itemInside.SetActive(true);

               DropObject(itemInside);
               Debug.Log($"Chest released {itemInside.name}");
          }

          // Drop the key (spawned prefab)
          if (keyPrefab != null)
          {
               GameObject key = Instantiate(keyPrefab);

               // Ensure it's visible and interactive
               key.SetActive(true);

               DropObject(key);
               Debug.Log("Chest released a key!");
          }
     }

     private void DropObject(GameObject obj)
     {
          // Always drop downward (adjust if you want left/right)
          Vector2 direction = Vector2.down;
          Vector2 spawnPos = (Vector2)transform.position + direction * spitDistance;

          obj.transform.position = spawnPos;

          // Apply a gentle push force
          Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
          if (rb != null)
               rb.AddForce(direction * spitForce, ForceMode2D.Impulse);

          // Trigger bounce effect if available
          BounceEffect bounce = obj.GetComponent<BounceEffect>();
          if (bounce != null && obj.activeInHierarchy)
               bounce.StartBounce();
     }
}

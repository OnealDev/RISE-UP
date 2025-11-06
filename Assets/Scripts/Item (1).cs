using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
     [Header("Item Settings")]
     public int ID;
     public string Name;
     public float massBonus = 0.5f; // how much this item increases the player's mass

     private bool canBePickedUp = false;
     private GameObject player;

     private void Start()
     {
          player = GameObject.FindGameObjectWithTag("Player");
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               canBePickedUp = true;
               Debug.Log($"{Name} is within pickup range.");
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               canBePickedUp = false;
               Debug.Log($"{Name} is out of range.");
          }
     }

     public bool CanInteract()
     {
          return canBePickedUp;
     }

     public void Interact()
     {
          if (canBePickedUp)
          {
               PickUp();
          }
     }

     public virtual void PickUp()
     {
          // Find the player
          GameObject player = GameObject.FindGameObjectWithTag("Player");

          if (player != null)
          {
               Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

               if (rb != null)
               {
                    // Increase mass (pushing power)
                    rb.mass += massBonus;

                    // Optional: visual feedback (grow slightly)
                    player.transform.localScale += new Vector3(0.03f, 0.03f, 0f);

                    Debug.Log($"{Name} picked up! Player mass is now {rb.mass:F2}");
               }
          }

          // Optional: show pickup UI message if you have one
          if (ItemPickupUIController.Instance != null)
          {
               SpriteRenderer sr = GetComponent<SpriteRenderer>();
               Sprite itemIcon = sr != null ? sr.sprite : null;
               ItemPickupUIController.Instance.ShowItemPickup($"{Name} collected! Mass +{massBonus}", itemIcon);
          }

          // Remove the item from the world
          Destroy(gameObject);
     }
}

using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
     [Header("Item Settings")]
     public int ID;
     public string Name;
     [Tooltip("Mass added to player on pickup.")]
     public float massBonus = 0.5f;

     private bool canBePickedUp = false;
     private GameObject player;

     private void Start()
     {
          player = GameObject.FindGameObjectWithTag("Player");
     }

     private void Update()
     {
          // Only pick up when the player presses E and is in range
          if (canBePickedUp && Input.GetKeyDown(KeyCode.E))
          {
               PickUp();
          }
     }

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               canBePickedUp = true;
               player = other.gameObject;
               Debug.Log($"Press E to pick up {Name}");
          }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.CompareTag("Player") && other.gameObject == player)
          {
               canBePickedUp = false;
               Debug.Log($"You walked away from {Name}");
          }
     }

     public bool CanInteract()
     {
          return canBePickedUp;
     }

     public void Interact()
     {
          if (canBePickedUp)
               PickUp();
     }

     public void PickUp()
     {
          if (player == null) return;

          // Apply strength/mass bonus
          Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
          if (rb != null && massBonus > 0f)
          {
               rb.mass += massBonus;

               var move = player.GetComponent<AryasPlayerMovement>();
               if (move != null)
               {
                    move.strength += massBonus;
                    Debug.Log($"{Name} picked up — mass +{massBonus}");
               }
          }

          Debug.Log($"You picked up: {Name}");
          Destroy(gameObject);
     }
}

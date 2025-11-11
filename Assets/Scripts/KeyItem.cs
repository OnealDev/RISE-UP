using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
     [Header("Key Settings")]
     public string keyName = "Key of Faith";  // Optional: name for console messages

     private bool canBePickedUp = false;
     private GameObject player;

     private void Start()
     {
          player = GameObject.FindGameObjectWithTag("Player");
     }

     private void Update()
     {
          // Wait for player to press E
          if (canBePickedUp && Input.GetKeyDown(KeyCode.E))
          {
               PickUp();
          }
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               canBePickedUp = true;
               player = collision.gameObject;
               Debug.Log($"Press E to pick up {keyName}");
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.CompareTag("Player") && collision.gameObject == player)
          {
               canBePickedUp = false;
               Debug.Log($"You walked away from {keyName}");
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

          // Optional: add to player's inventory if that system exists
          PlayerInventory inventory = player.GetComponent<PlayerInventory>();
          if (inventory != null)
          {
               inventory.AddKey();
          }

          Debug.Log($"{keyName} picked up!");
          Destroy(gameObject);
     }
}

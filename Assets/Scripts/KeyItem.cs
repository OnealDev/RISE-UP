using UnityEngine;

public class KeyItem : MonoBehaviour
{
     private bool canBePickedUp = false;
     private PlayerInventory inventory;

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               inventory = collision.GetComponent<PlayerInventory>();

               if (inventory != null)
               {
                    canBePickedUp = true;
                    Debug.Log("Press E to pick up the Key.");
               }
          }
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               canBePickedUp = false;
          }
     }

     private void Update()
     {
          if (canBePickedUp && Input.GetKeyDown(KeyCode.E))
          {
               inventory.AddKey();
               Destroy(gameObject);   // Removes the key from the world
          }
     }
}

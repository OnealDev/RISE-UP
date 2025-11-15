using UnityEngine;

public class KeyPickup : MonoBehaviour
{
     private bool canBePickedUp = false;
     private GameObject player;

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               canBePickedUp = true;
               player = other.gameObject;
          }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               canBePickedUp = false;
               player = null;
          }
     }

     private void Update()
     {
          if (canBePickedUp && Input.GetKeyDown(KeyCode.E))
          {
               PlayerInventory inv = player.GetComponent<PlayerInventory>();

               if (inv != null)
               {
                    inv.AddKey();          // use your existing method
               }

               Destroy(gameObject);       // remove key from world
          }
     }
}

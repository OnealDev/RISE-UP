using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
     private bool canBePickedUp = false;
     private GameObject player;

     [Header("Popup Settings")]
     public GameObject popupTextPrefab; // assign your PopupText prefab in Inspector

     private void Start()
     {
          player = GameObject.FindGameObjectWithTag("Player");
     }

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
               canBePickedUp = true;
     }

     private void OnTriggerExit2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
               canBePickedUp = false;
     }

     public bool CanInteract()
     {
          return canBePickedUp;
     }

     public void Interact()
     {
          if (!canBePickedUp) return;

          // Add your logic for what happens when key is picked up
          PlayerInventory inventory = player.GetComponent<PlayerInventory>();
          if (inventory != null)
          {
               inventory.AddKey(); // add the key to player inventory
          }

          // Create a popup text message
          if (popupTextPrefab != null)
          {
               GameObject popup = Instantiate(
                   popupTextPrefab,
                   player.transform.position + Vector3.up * 1.5f,
                   Quaternion.identity
               );

               popup.GetComponent<PopupText>().SetText("You found a key!");
          }

          Debug.Log("Player picked up a key!");
          Destroy(gameObject);
     }
}


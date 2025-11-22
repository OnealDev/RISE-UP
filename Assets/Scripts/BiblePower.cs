using UnityEngine;

public class BiblePower : MonoBehaviour, IInteractable
{
     [Header("Bible Settings")]
     public float damageBoost = 1f;

     private bool canBePickedUp = false;
     private GameObject player;

     private void Start()
     {
          player = GameObject.FindGameObjectWithTag("Player");
     }

     private void Update()
     {
          // Require E key to pick up
          if (canBePickedUp && Input.GetKeyDown(KeyCode.E))
          {
               PickUp();
          }
     }

     // This ONLY allows interaction — does NOT auto-pickup
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               canBePickedUp = true;
               player = other.gameObject;
               Debug.Log("Press E to pick up Bible");
          }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
          if (other.CompareTag("Player") && other.gameObject == player)
          {
               canBePickedUp = false;
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

          // DAMAGE ONLY
          Player_Combat combat = player.GetComponent<Player_Combat>();
          if (combat != null)
               combat.damage += (int)damageBoost;

          Debug.Log("Bible picked up — damage increased!");

          Destroy(gameObject);
     }
}

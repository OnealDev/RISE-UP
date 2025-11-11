using UnityEngine;

public class ItemPopup : MonoBehaviour
{
     [Header("Popup Settings")]
     public GameObject popupTextPrefab;
     [TextArea]
     public string popupMessage = "You picked up an item!";

     [Header("Popup Behavior")]
     public Vector3 popupOffset = new Vector3(0, 1f, 0);

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               ShowPopup();
               Destroy(gameObject); // remove item after pickup
          }
     }

     private void ShowPopup()
     {
          if (popupTextPrefab != null)
          {
               GameObject popup = Instantiate(popupTextPrefab, transform.position + popupOffset, Quaternion.identity);
               var popupText = popup.GetComponent<PopupText>();
               if (popupText != null)
                    popupText.SetText(popupMessage);
          }
     }
}

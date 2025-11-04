using UnityEngine;
using TMPro;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
     public string itemName = "Bible";
     public GameObject popupPrefab; // Assign ItemPopUP here (not the UI Canvas)
     public float messageDuration = 3f;

     private bool collected = false;

     void OnTriggerEnter2D(Collider2D other)
     {
          if (!collected && other.CompareTag("Player"))
          {
               collected = true;

               // ✅ Create popup under the main Canvas
               GameObject popup = Instantiate(popupPrefab, Object.FindFirstObjectByType<Canvas>().transform, false);
               popup.SetActive(true);

               TMP_Text popupText = popup.GetComponentInChildren<TMP_Text>();
               if (popupText != null)
                    popupText.text = $"{itemName} collected! You've been blessed.";

               StartCoroutine(HideAndDestroyPopup(popup));

               // Hide the Bible object
               gameObject.SetActive(false);
          }
     }

     private IEnumerator HideAndDestroyPopup(GameObject popup)
     {
          yield return new WaitForSeconds(messageDuration);
          Destroy(popup);
     }
}



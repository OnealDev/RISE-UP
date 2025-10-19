using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
     public string itemName = "Scripture";
     public GameObject messagePanel;
     public TMP_Text messageText;
     public float messageDuration = 3f;

     private bool collected = false;

     void OnTriggerEnter2D(Collider2D other)
     {
          if (!collected && other.CompareTag("Player"))
          {
               collected = true;

               // Show message before deactivating object
               if (messagePanel != null && messageText != null)
               {
                    messagePanel.SetActive(true);
                    messageText.text = $"{itemName} collected! You've been blessed.";

                    // Start coroutine while still active
                    StartCoroutine(HideMessageAfterDelay());
               }

               // Now hide the book
               gameObject.SetActive(false);
          }
     }

     private IEnumerator HideMessageAfterDelay()
     {
          yield return new WaitForSeconds(messageDuration);
          if (messagePanel != null)
               messagePanel.SetActive(false);
     }
}

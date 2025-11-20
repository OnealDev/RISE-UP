using UnityEngine;

public class BiblePower : MonoBehaviour
{
     public float damageBoost = 1f;
     public GameObject popupTextPrefab;

     private void OnTriggerEnter2D(Collider2D other)
     {
          AryasPlayerMovement player = other.GetComponent<AryasPlayerMovement>();
          if (player != null)
          {
               // Boost strength
               player.strength += damageBoost * 0.5f;

               // Boost damage
               Player_Combat combat = player.GetComponent<Player_Combat>();
               if (combat != null)
               {
                    combat.damage += (int)damageBoost;
               }

               // Verse Popup
               if (popupTextPrefab != null)
               {
                    GameObject popup = Instantiate(
                        popupTextPrefab,
                        player.transform.position + Vector3.up * 1.5f,
                        Quaternion.identity
                    );
                    popup.GetComponent<PopupText>().SetText(
                        "“Blessed be the Lord, my rock, who trains my hands for war, and my fingers for battle;”\n‭‭Psalm‬ ‭144‬:‭1‬ ‭ESV‬‬"
                    );
               }

               Destroy(gameObject);
          }
     }
}

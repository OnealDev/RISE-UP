using UnityEngine;

public class HeartPickup : MonoBehaviour
{
     public int healAmount = 1;        // How much health it restores
     public AudioClip pickupSound;     // Add your sound here
     public float soundVolume = 1f;     // Adjust pickup sound volume

     private void OnTriggerEnter2D(Collider2D collision)
     {
          // Check if the player touched it
          HealthManager health = collision.GetComponent<HealthManager>();
          if (health != null)
          {
               health.Heal(healAmount);   // Heal the player

               // Play the pickup sound **at the heart's position**
               if (pickupSound != null)
               {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
               }

               Destroy(gameObject,0.2f);       // Remove the heart after pickup
          }
     }
}

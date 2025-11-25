using UnityEngine;

public class InteractiveFlame : MonoBehaviour
{
     // -----------------------------
     // EXTINGUISH SETTINGS
     // -----------------------------
     [Header("Extinguish Settings")]
     public AudioClip extinguishSound;
     public float soundVolume = 1f;

     private AudioSource audioSource;
     private bool isExtinguished = false;

     // -----------------------------
     // DAMAGE + KNOCKBACK SETTINGS
     // -----------------------------
     [Header("Damage Settings")]
     public int damage = 1;                // How much damage the flame deals
     public float damageCooldown = 1f;     // How often the flame can hurt the player
     private float lastDamageTime = 0f;    // Internal cooldown tracker

     void Awake()
     {
          // Add / confirm AudioSource
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
               audioSource = gameObject.AddComponent<AudioSource>();

          audioSource.playOnAwake = false;
     }

     // -----------------------------
     // EXTINGUISH FUNCTION
     // -----------------------------
     public void Extinguish()
     {
          if (isExtinguished) return; // Prevent duplicates
          isExtinguished = true;

          // Play extinguish sound
          if (extinguishSound != null)
               audioSource.PlayOneShot(extinguishSound, soundVolume);

          // Destroy after audio finishes
          Destroy(gameObject, extinguishSound != null ? extinguishSound.length : 0f);
     }

     // -----------------------------
     // PROXIMITY DAMAGE + KNOCKBACK
     // -----------------------------
     private void OnTriggerStay2D(Collider2D collision)
     {
          // If flame is extinguished, it no longer hurts
          if (isExtinguished) return;

          // Only hurt the player
          if (!collision.CompareTag("Player")) return;

          // Damage cooldown check
          if (Time.time < lastDamageTime + damageCooldown)
               return;

          // Get player's HealthManager
          HealthManager playerHealth = collision.GetComponent<HealthManager>();
          if (playerHealth != null)
          {
               // Knockback direction: push AWAY from flame
               Vector2 knockDirection = (collision.transform.position - transform.position).normalized;

               // Use your built-in gamewide damage system
               playerHealth.TakeDamage(damage, knockDirection);
          }

          lastDamageTime = Time.time;
     }
}

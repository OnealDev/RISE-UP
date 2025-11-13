using UnityEngine;
using UnityEngine.UI;

public class DemonHealth : MonoBehaviour
{
     [Header("Health Settings")]
     public int maxHealth = 100;
     private int currentHealth;
     private bool isDead = false;

     [Header("Health Bar UI (World Space)")]
     [Tooltip("Assign a world-space Slider here (child of a World Space Canvas).")]
     public Slider healthSlider;
     public Vector3 healthBarOffset = new Vector3(0, 2f, 0); // Offset above the Demon’s head

     private DemonAI demonAI;
     private DemonHitSound demonHitSound;
     private FlashOnHit flashOnHit;
     private Camera mainCam;

     void Start()
     {
          currentHealth = maxHealth;

          demonAI = GetComponent<DemonAI>();
          demonHitSound = GetComponent<DemonHitSound>();
          flashOnHit = GetComponent<FlashOnHit>();
          mainCam = Camera.main;

          // Initialize the health bar
          if (healthSlider != null)
          {
               healthSlider.maxValue = maxHealth;
               healthSlider.value = maxHealth;
          }
     }

     void Update()
     {
          // Make the health bar follow the Demon and face the camera
          if (healthSlider != null && mainCam != null)
          {
               // Keep the health bar positioned just above the Demon
               Vector3 worldPos = transform.position + healthBarOffset;
               healthSlider.transform.position = worldPos;

               // Rotate the health bar so it always faces the camera
               healthSlider.transform.LookAt(healthSlider.transform.position + mainCam.transform.forward);
          }
     }

     /// <summary>
     /// Apply health changes (negative = damage, positive = heal)
     /// </summary>
     public void ChangeHealth(int amount)
     {
          if (isDead) return;

          currentHealth += amount;
          currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

          if (amount < 0)
          {
               // Flash red when hit
               flashOnHit?.Flash();

               // Play hurt sound
               demonHitSound?.PlayHurtSound();

               //  Apply knockback when taking damage
               DemonKnockback demonKnockback = GetComponent<DemonKnockback>();
               if (demonKnockback != null)
               {
                    // Optional: use DemonAI's player reference for direction
                    if (demonAI != null && demonAI.player != null)
                    {
                         Vector2 direction = (transform.position - demonAI.player.position).normalized;
                         demonKnockback.ApplyKnockback(direction);
                    }
               }

               Debug.Log($" Demon took {Mathf.Abs(amount)} damage! HP: {currentHealth}/{maxHealth}");
          }

          // Update the health bar
          if (healthSlider != null)
               healthSlider.value = currentHealth;

          if (currentHealth <= 0 && !isDead)
               Die();
     }


     private void Die()
     {
          if (isDead) return;
          isDead = true;

          Debug.Log(" Demon has been slain!");

          // Play death sound
          demonHitSound?.PlayDeathSound();

          // Trigger death animation
          if (demonAI != null)
          {
               demonAI.Die();
          }
          else
          {
               Destroy(gameObject);
          }

          // Optionally fade or destroy the health bar
          if (healthSlider != null)
               Destroy(healthSlider.gameObject, 1.5f);
     }

     // Optional healing
     public void Heal(int amount)
     {
          if (isDead) return;

          currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
          if (healthSlider != null)
               healthSlider.value = currentHealth;
     }
}

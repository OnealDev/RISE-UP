using UnityEngine;

public class EnemyVitals : MonoBehaviour
{
     [Header("Health Settings")]
     public int maxHealth = 3;
     public int currentHealth;

     [Header("Effects")]
     private FlashOnHit flash;
     private HitSound hitSound;

     private void Awake()
     {
          currentHealth = maxHealth;
          flash = GetComponent<FlashOnHit>();
          hitSound = GetComponent<HitSound>();
     }

     public void ChangeHealth(int amount)
     {
          currentHealth += amount;

          if (currentHealth > maxHealth)
               currentHealth = maxHealth;

          if (amount < 0)
          {
               if (flash != null)
                    flash.Flash();

               if (hitSound != null)
                    hitSound.PlayRandomHitSound();
          }

          if (currentHealth <= 0)
               Die();
     }

     private void Die()
     {
          // Count the enemy kill
          if (EnemyKillManager.Instance != null)
               EnemyKillManager.Instance.RegisterKill();

          float delay = 0f;

          if (hitSound != null && hitSound.hitSound != null)
          {
               hitSound.PlayRandomHitSound();
               delay = hitSound.hitSound.length;
          }

          if (ScreenShake.Instance != null)
               ScreenShake.Instance.BigShake();

          // Disable visuals
          foreach (var r in GetComponentsInChildren<SpriteRenderer>())
               r.enabled = false;

          // Disable colliders
          foreach (var c in GetComponentsInChildren<Collider2D>())
               c.enabled = false;

          // Disable all other scripts
          foreach (var script in GetComponents<MonoBehaviour>())
          {
               if (script != this)
                    script.enabled = false;
          }

          Destroy(gameObject, delay);
     }
}

using UnityEngine;

public class Fire_Trap : MonoBehaviour
{
     [Header("Damage Settings")]
     public int damageAmount = 1;
     public float damageCooldown = 1f;

     [Header("Knockback Settings")]
     public float knockbackForce = 8f;

     private float lastDamageTime = 0f;

     [Header("Animation Settings")]
     public Sprite[] flameFrames;   // Only assign the flame ON frames
     private SpriteRenderer sr;

     private void Awake()
     {
          sr = GetComponent<SpriteRenderer>();
     }

     private void OnTriggerStay2D(Collider2D collision)
     {
          if (!collision.CompareTag("Player"))
               return;

          // Only damage when a flame frame is active
          if (!IsFlameActive())
               return;

          // Cooldown between hits
          if (Time.time - lastDamageTime < damageCooldown)
               return;

          lastDamageTime = Time.time;

          // --- DAMAGE ---
          HealthManager hm = collision.GetComponent<HealthManager>();
          if (hm != null)
          {
               Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
               hm.TakeDamage(damageAmount, knockbackDirection);
          }
          // --- KNOCKBACK ---
          Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
          if (rb != null)
          {
               Vector2 direction = (collision.transform.position - transform.position).normalized;
               rb.linearVelocity = direction * knockbackForce;
          }
     }

     private bool IsFlameActive()
     {
          foreach (Sprite flame in flameFrames)
          {
               if (sr.sprite == flame)
                    return true;
          }
          return false;
     }
}

using UnityEngine;

public class DemonHitbox : MonoBehaviour
{
     [Header("Damage Settings")]
     public int damageAmount = 2;
     public float attackCooldown = 2f;

     private float lastAttackTime = -999f;

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (!enabled) return;

          if (other.CompareTag("Player"))
          {
               if (Time.time - lastAttackTime >= attackCooldown)
               {
                    HealthManager health = other.GetComponent<HealthManager>();
                    if (health != null)
                    {
                         Vector2 knockbackDir = (other.transform.position - transform.position).normalized;
                         health.TakeDamage(damageAmount, knockbackDir);
                    }

                    lastAttackTime = Time.time;
               }
          }
     }
}

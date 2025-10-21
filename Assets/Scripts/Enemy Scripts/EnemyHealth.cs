using UnityEngine;


public class EnemyHealth : MonoBehaviour
{

     public Vector2 lastMoveDir = Vector2.down; // default facing down

     public int currentHealth;
     public int maxHealth;

     private void Start()
     {
          currentHealth = maxHealth;
     }
    
    public void ChangeHealth(int amount)
     {
          currentHealth += amount;

          if (currentHealth > maxHealth)
          {
               currentHealth = maxHealth;
          }
        else if(currentHealth <= 0)
          {
               Destroy(gameObject);
        }
    }
     /*
     [Header("Health Settings")]
     public int maxHealth = 3;
     private int currentHealth;

     private Animator animator;

     // Track last direction (set by EnemyFollow or movement script)
     public Vector2 lastMoveDir = Vector2.down; // default facing down

     [Header("Death Settings")]
     public float deathDelay = 0.8f; // seconds before destroying object

     void Start()
     {
          currentHealth = maxHealth;
          animator = GetComponent<Animator>();
     }

     public void TakeDamage(int damage)
     {
          Debug.Log($"{gameObject.name} took {damage} damage! Current health: {currentHealth - damage}");

          currentHealth -= damage;

          if (currentHealth <= 0)
          {
               Die();
          }
     }

     void Die()
     {
          Debug.Log($"{gameObject.name} has died.");

          if (animator != null)
          {
               // Choose death animation based on last movement direction
               if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
               {
                    if (lastMoveDir.x > 0)
                         animator.Play("LavaSlime_DeathRight");
                    else
                         animator.Play("LavaSlime_DeathLeft");
               }
               else
               {
                    if (lastMoveDir.y > 0)
                         animator.Play("LavaSlime_DeathBack");
                    else
                         animator.Play("LavaSlime_DeathFront");
               }

               // Destroy after delay
               Destroy(gameObject, deathDelay);
          }
          else
          {
               Destroy(gameObject);
          }
     }
*/

}



using UnityEngine;

public class DemonHealth : MonoBehaviour
{
     public int maxHealth = 5;
     private int currentHealth;
     private Animator anim;
     private Rigidbody2D rb;

     void Start()
     {
          currentHealth = maxHealth;
          anim = GetComponent<Animator>();
          rb = GetComponent<Rigidbody2D>();
     }

     public void TakeDamage(int amount)
     {
          currentHealth -= amount;
          currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

          if (currentHealth <= 0)
          {
               Die();
          }
     }

     private void Die()
     {
          anim.SetBool("isDead", true); // triggers Demon Death Animation
          rb.linearVelocity = Vector2.zero;
          GetComponent<DemonAI>().enabled = false; // stop AI
          GetComponent<Collider2D>().enabled = false; // stop collisions
     }
}

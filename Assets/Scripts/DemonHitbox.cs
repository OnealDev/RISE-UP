using UnityEngine;

public class DemonHitbox : MonoBehaviour
{
     [Header("Damage Settings")]
     public int damage = 1; // how much damage the demon deals per hit

     private void OnTriggerEnter2D(Collider2D collision)
     {
          // Check if we hit the player
          if (collision.CompareTag("Player"))
          {
               // Try to get HealthManager from the player
               HealthManager health = collision.GetComponent<HealthManager>();
               if (health != null)
               {
                    health.TakeDamage(damage);
                    Debug.Log("Player hit by demon!");
               }
          }
     }
}

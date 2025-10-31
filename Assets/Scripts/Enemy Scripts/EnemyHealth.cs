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

}



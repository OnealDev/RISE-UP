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

          // FLASH EFFECT WHEN TAKING DAMAGE
          if (amount < 0) // flash when taking damage (negative amount)
          {
               Debug.Log($"💥 Enemy took damage! Calling flash...");
        
               FlashOnHit flash = GetComponent<FlashOnHit>();
               if (flash != null)
               {
                flash.Flash();
               }
        else
        {
            Debug.LogError("❌ No FlashOnHit component found!");
        }
               GetComponent<HitSound>()?.PlayRandomHitSound();
          }
          
        
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



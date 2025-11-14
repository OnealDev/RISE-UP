using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Vector2 lastMoveDir = Vector2.down;
    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if (amount < 0) // Taking damage
        {
            Debug.Log($"💥 Enemy took damage! Calling flash...");
            
            // Flash effect
            FlashOnHit flash = GetComponent<FlashOnHit>();
            if (flash != null)
            {
                flash.Flash();
            }
            
            // Play hit sound
            HitSound hitSound = GetComponent<HitSound>();
            if (hitSound != null)
            {
                hitSound.PlayRandomHitSound();
            }
        }
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play the hit sound one more time for death
        HitSound hitSound = GetComponent<HitSound>();
        if (hitSound != null)
        {
            hitSound.PlayRandomHitSound();
        }
        
        // Screen shake for death
        if (ScreenShake.Instance != null)
        {
            ScreenShake.Instance.BigShake();
        }
        
        Destroy(gameObject);
    }
}
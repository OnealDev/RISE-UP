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
            Debug.Log($" Enemy took damage! Calling flash...");
            
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
     //Neal 
     // COUNT THIS KILL (important!)
     if (EnemyKillTracker.Instance != null)
               EnemyKillTracker.Instance.AddKill();

          // Play the hit sound one more time
          HitSound hitSound = GetComponent<HitSound>();
    float delay = 0f;

    // Play hit sound only if clip exists in inspector
    if (hitSound != null && hitSound.hitSound != null)
    {
        hitSound.PlayRandomHitSound();
        delay = hitSound.hitSound.length;
    }
    // Screen shake for death
    if (ScreenShake.Instance != null)
        ScreenShake.Instance.BigShake();

    //Now we hide the enemy

    // Disable renderer
    foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        renderer.enabled = false;

    // Disable collider
    foreach (var col in GetComponentsInChildren<Collider2D>())
        col.enabled = false;

    // Disable movement
    foreach (var behaviour in GetComponents<MonoBehaviour>())
    {
        if (behaviour != this && behaviour.enabled)
            behaviour.enabled = false;
    }

    
    Destroy(gameObject, delay);
}

}
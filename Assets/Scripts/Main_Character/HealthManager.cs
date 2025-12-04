using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Debug Options")]
    public bool enableDebugKeys = true; // Toggle debug features on/off

    [Header("Heart UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    private AryasPlayerMovement movementScript; //We need to reference the movement script when he takes damage

    public GameOverManager gameOverManager; 
    [Header("Invincibility Settings")]
    public float invincibilityTime = 1f;  
    private bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<AryasPlayerMovement>(); 
    }

    void Update()
    {
        // Debug health addition when F key is pressed
        if (enableDebugKeys && Input.GetKeyDown(KeyCode.F))
        {
            AddDebugHealth();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;  // <-- blocks damage if invincible
        TakeDamage(amount, Vector2.zero); // Calls the existing method with no knockback
        StartCoroutine(InvincibilityCoroutine());
    }
    
    public void TakeDamage(int amount, Vector2 damageDirection)
    {
        if (isInvulnerable) return;  // <-- blocks damage if invincible
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        StartCoroutine(InvincibilityCoroutine());
        //Damage effects
        if (amount > 0)
        {
            
            FlashOnHit flash = GetComponent<FlashOnHit>();
            if (flash != null)
            {
                flash.Flash();
            }
            
           
            HitSound hitSound = GetComponent<HitSound>();
            if (hitSound != null)
            {
                hitSound.PlayRandomHitSound();
            }
            
           
            if (ScreenShake.Instance != null)
            {
                ScreenShake.Instance.QuickShake();
            }

            
            ApplyKnockback(damageDirection);
        }

        UpdateHearts();

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void ApplyKnockback(Vector2 direction)
    {   
        //If knockback is already being applied just exit the function to prevent multiple knockbacks
        if (isKnockedBack) 
            return; 

        //Only apply knockback if we have a direction to go to 
        if (direction != Vector2.zero && rb != null) 
        {
            isKnockedBack = true;
            
            //Disable movement script during knockback
            if (movementScript != null)
            {
                movementScript.enabled = false;
            }
            
            //Apply knockback 
            rb.linearVelocity = direction.normalized * knockbackForce;
            
            //Stop knockback after a certain duration duration
            StartCoroutine(StopKnockbackAfterDelay());
        }
    }

    private IEnumerator StopKnockbackAfterDelay()
    {
        yield return new WaitForSeconds(knockbackDuration);
        
        isKnockedBack = false;
        
        //Re-enable movement script
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }
        
        //Reset direction
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    private void PlayerDeath()
    {
        if (ScreenShake.Instance != null)
        {
            ScreenShake.Instance.BigShake();
        }
        
        Debug.Log("PLAYER DIED!");
        gameObject.SetActive(false);
        // Later: add respawn, animation, sound, etc.

        // Show Game Over screen
        if (gameOverManager != null)
        {
           gameOverManager.ShowGameOver();
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvulnerable = true;

        FlashOnHit flash = GetComponent<FlashOnHit>();
        if (flash != null)
            StartCoroutine(flash.StartInvincibilityFlash(invincibilityTime));

        yield return new WaitForSeconds(invincibilityTime);

        isInvulnerable = false;
    }

    // Debug function to add health with F key
    private void AddDebugHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHearts();
            Debug.Log($"Debug: Added health. Current health: {currentHealth}/{maxHealth}");
        }
        else
        {
            Debug.Log($"Debug: Health already full ({currentHealth}/{maxHealth})");
        }
    }
}
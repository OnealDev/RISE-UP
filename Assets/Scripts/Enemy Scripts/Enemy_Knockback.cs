using UnityEngine;
using System.Collections;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyFollow movement;
    private bool isKnockedBack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyFollow>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        if (isKnockedBack) return; // Prevent multiple knockbacks
        
        StartCoroutine(KnockbackCoroutine(forceTransform, knockbackForce, knockbackTime, stunTime));
    }

    private IEnumerator KnockbackCoroutine(Transform forceTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        isKnockedBack = true;
        
        // Disable enemy movement during knockback
        if (movement != null)
            movement.enabled = false;

        // Calculate knockback direction
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
        
        Debug.Log("Knockback applied with force: " + (direction * knockbackForce));

        // Wait for knockback duration
        yield return new WaitForSeconds(knockbackTime);
        
        // Stop the knockback velocity
        rb.linearVelocity = Vector2.zero;
        
        // Additional stun time (if any)
        if (stunTime > knockbackTime)
        {
            yield return new WaitForSeconds(stunTime - knockbackTime);
        }

        // Re-enable movement
        if (movement != null)
            movement.enabled = true;
            
        isKnockedBack = false;
    }
}
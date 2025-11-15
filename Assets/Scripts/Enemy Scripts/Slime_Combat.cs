using UnityEngine;

public class Slime_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform slimeAttackHitbox; 

    
    public void EnableAttackHitbox()
    {
        if (slimeAttackHitbox != null)
            slimeAttackHitbox.gameObject.SetActive(true);
    }

    public void DisableAttackHitbox()
    {
        if (slimeAttackHitbox != null)
            slimeAttackHitbox.gameObject.SetActive(false);
    }

    //Attack hitbox 
    public void OnHitboxTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage(collision.transform);
        }
    }

    //body collision 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DealDamage(collision.transform);
        }
    }

    private void DealDamage(Transform target)
    {
        HealthManager playerHealth = target.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            Vector2 knockbackDirection = (target.position - transform.position).normalized;
            playerHealth.TakeDamage(damage, knockbackDirection);
        }
    }

   
    private void OnDrawGizmosSelected()
{
    if (slimeAttackHitbox != null)
    {
        Gizmos.color = Color.red;
        CircleCollider2D collider = slimeAttackHitbox.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            
            Gizmos.DrawWireSphere(slimeAttackHitbox.position, collider.radius);
        }
    }
}
}
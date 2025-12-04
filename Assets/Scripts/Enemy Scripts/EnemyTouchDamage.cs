using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerHealth.TakeDamage(damage, knockbackDir);
            }
        }
    }
}
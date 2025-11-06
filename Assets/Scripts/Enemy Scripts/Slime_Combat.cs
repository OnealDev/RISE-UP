using UnityEngine;

public class Slime_Combat : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only damage the player
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
            }
        }
    }
}
using UnityEngine;

public class Goblin_Combat : MonoBehaviour
{
    public int damage = 1;
    
    [Header("Attack Hitboxes")]
    public Transform attackHitboxUp;
    public Transform attackHitboxDown;
    public Transform attackHitboxLeft;
    public Transform attackHitboxRight;

    [Header("Attack Direction")]
    [SerializeField] private Vector2 lastMovementDirection = Vector2.down; // Default to facing down
    private EnemyFollow enemyFollow; // Reference to your existing follow script

    void Start()
    {
        // Get the EnemyFollow script to access movement direction
        enemyFollow = GetComponent<EnemyFollow>();
        
        // Disable all hitboxes initially
        DisableAllHitboxes();
    }

    // Called by animation events to enable the correct hitbox based on direction
    public void EnableAttackHitbox()
    {
        // Use the last movement direction from EnemyFollow script
        if (enemyFollow != null)
        {
            // Try to get direction from animator parameters
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                float moveX = anim.GetFloat("MoveX");
                float moveY = anim.GetFloat("MoveY");
                lastMovementDirection = new Vector2(moveX, moveY).normalized;
                
                // If animator values are near zero, check for last idle direction
                if (lastMovementDirection.magnitude < 0.1f)
                {
                    // We'll need to add a public method to EnemyFollow to get lastIdleDir
                    lastMovementDirection = GetLastDirectionFromEnemyFollow();
                }
            }
        }
        
        // Disable all hitboxes first
        DisableAllHitboxes();
        
        // Enable the appropriate hitbox based on direction
        EnableHitboxBasedOnDirection();
    }

    // Method to get direction from EnemyFollow (you'll need to modify EnemyFollow slightly)
    private Vector2 GetLastDirectionFromEnemyFollow()
    {
        // Since lastIdleDir is private in EnemyFollow, we have a few options:
        
        // Option 1: Add this method to EnemyFollow to expose lastIdleDir
        // public Vector2 GetLastDirection() { return lastIdleDir; }
        
        // Option 2: Use reflection (not recommended for production)
        // System.Reflection.FieldInfo field = typeof(EnemyFollow).GetField("lastIdleDir", 
        //     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        // return (Vector2)field.GetValue(enemyFollow);
        
        // Option 3: Try to get from animator idle parameters
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            return new Vector2(
                anim.GetFloat("MoveX"),
                anim.GetFloat("MoveY")
            ).normalized;
        }
        
        return Vector2.down; // Default fallback
    }

    private void EnableHitboxBasedOnDirection()
    {
        // Get absolute values to determine primary direction
        float absX = Mathf.Abs(lastMovementDirection.x);
        float absY = Mathf.Abs(lastMovementDirection.y);
        
        // If both directions are zero, default to down
        if (absX < 0.1f && absY < 0.1f)
        {
            lastMovementDirection = Vector2.down;
            absY = 1f;
        }
        
        // Check which direction is stronger
        if (absX > absY)
        {
            // Horizontal direction is stronger
            if (lastMovementDirection.x > 0)
            {
                if (attackHitboxRight != null) 
                    attackHitboxRight.gameObject.SetActive(true);
            }
            else
            {
                if (attackHitboxLeft != null) 
                    attackHitboxLeft.gameObject.SetActive(true);
            }
        }
        else
        {
            // Vertical direction is stronger
            if (lastMovementDirection.y > 0)
            {
                if (attackHitboxUp != null) 
                    attackHitboxUp.gameObject.SetActive(true);
            }
            else
            {
                if (attackHitboxDown != null) 
                    attackHitboxDown.gameObject.SetActive(true);
            }
        }
    }

    // Called by animation events to disable all hitboxes
    public void DisableAllHitboxes()
    {
        if (attackHitboxUp != null) attackHitboxUp.gameObject.SetActive(false);
        if (attackHitboxDown != null) attackHitboxDown.gameObject.SetActive(false);
        if (attackHitboxLeft != null) attackHitboxLeft.gameObject.SetActive(false);
        if (attackHitboxRight != null) attackHitboxRight.gameObject.SetActive(false);
    }

    // Method to manually set attack direction
    public void SetAttackDirection(Vector2 direction)
    {
        lastMovementDirection = direction.normalized;
    }

    // Generic method for any hitbox trigger
    public void OnHitboxTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage(collision.transform);
        }
    }

    // Body collision - REMOVED knockback to prevent goblin from flying away
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only apply damage, NO knockback on simple body collision
            HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                // Damage only, no knockback from body collision
                playerHealth.TakeDamage(damage);
            }
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

    // Gizmos for visualizing hitboxes in the editor
    private void OnDrawGizmosSelected()
    {
        DrawHitboxGizmo(attackHitboxUp, Color.red);
        DrawHitboxGizmo(attackHitboxDown, Color.blue);
        DrawHitboxGizmo(attackHitboxLeft, Color.green);
        DrawHitboxGizmo(attackHitboxRight, Color.yellow);
    }

    private void DrawHitboxGizmo(Transform hitbox, Color color)
    {
        if (hitbox != null)
        {
            Gizmos.color = color;
            CircleCollider2D collider = hitbox.GetComponent<CircleCollider2D>();
            if (collider != null)
            {
                Gizmos.DrawWireSphere(hitbox.position, collider.radius);
            }
        }
    }
}
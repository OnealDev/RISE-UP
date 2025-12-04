using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [Header("References")]
    public Transform followTarget;       // Player to follow
    private Rigidbody2D rb2D;
    private Animator anim;
    private EnemyHealth enemyHealth;

    [Header("Movement & Attack")]
    public float followSpeed = 2f;       // Movement speed
    public float detectionRange = 5f;    // How far the slime notices the player
    public float attackRange = 1.5f;     // Distance to start attack
    public float attackCooldown = 2f;    // Seconds between attacks

    private Vector2 lastIdleDir = Vector2.down; // Last move direction
    private bool isAttacking = false;           // Track attack state
    private float lastAttackTime = 0f;          // Track cooldown
    private Vector3 originalPosition;          

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        originalPosition = transform.position; //Save starting position
    }

    void FixedUpdate()
    {
        if (followTarget == null || isAttacking)
            return;

        float distanceToTarget = Vector2.Distance(transform.position, followTarget.position);

        // Check if player is within detection range
        if (distanceToTarget <= detectionRange)
        {
            
            if (distanceToTarget <= attackRange && CanAttack())
            {
                StartAttack();
            }
            
            else if (distanceToTarget > attackRange)
            {
                ChasePlayer();
            }
        }
        else
        {
            //Return to original position
            ReturnToOriginalPosition();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (followTarget.position - transform.position).normalized;

        if (enemyHealth != null)
            enemyHealth.lastMoveDir = direction;

        rb2D.MovePosition(rb2D.position + direction * followSpeed * Time.fixedDeltaTime);

        if (anim != null)
        {
            anim.SetFloat("MoveX", direction.x);
            anim.SetFloat("MoveY", direction.y);
            anim.SetFloat("Speed", direction.sqrMagnitude);
        }

        if (direction.sqrMagnitude > 0.01f)
            lastIdleDir = direction;
    }

    
    private void ReturnToOriginalPosition()
    {
        float distanceToOriginal = Vector2.Distance(transform.position, originalPosition);
        
        // If already at original position go idle
        if (distanceToOriginal < 0.1f)
        {
            rb2D.linearVelocity = Vector2.zero;
            if (anim != null)
            {
                anim.SetFloat("Speed", 0);
                anim.SetFloat("MoveX", lastIdleDir.x);
                anim.SetFloat("MoveY", lastIdleDir.y);
            }
        }
        else
        {
            
            Vector2 direction = (originalPosition - transform.position).normalized;
            
            rb2D.MovePosition(rb2D.position + direction * followSpeed * Time.fixedDeltaTime);

            if (anim != null)
            {
                anim.SetFloat("MoveX", direction.x);
                anim.SetFloat("MoveY", direction.y);
                anim.SetFloat("Speed", direction.sqrMagnitude);
            }

            if (direction.sqrMagnitude > 0.01f)
                lastIdleDir = direction;
        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void StartAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Stop movement
        rb2D.linearVelocity = Vector2.zero;
        if (anim != null)
            anim.SetFloat("Speed", 0);

        // Trigger attack animation
        if (anim != null)
            anim.SetTrigger("AttackTrigger");
    }

    // Called by animation event at end of attack
    public void OnAttackEnd()
    {
        isAttacking = false;
    }

    public Vector2 GetLastDirection()
{
    return lastIdleDir;
}
}
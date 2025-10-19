using UnityEngine;

public class WalkAndAttackAndFollow : MonoBehaviour
{
     [Header("Target Settings")]
     public Transform target; // Priest to attack

     [Header("Movement Settings")]
     public float moveSpeed = 2f;
     public float stoppingDistance = 1.5f;

     [Header("Attack Settings")]
     public float attackRange = 1.5f;
     public float attackCooldown = 2f;

     [Header("Detection Settings")]
     public float detectionRange = 8f; // how far Demon King can detect priest

     private float lastAttackTime;
     private Rigidbody2D rb;
     private Animator animator;

     // --- NEW: store starting (home) position ---
     private Vector2 homePosition;
     private bool returningHome = false;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();
          homePosition = transform.position; // Save Demon King's spawn as home
     }

     void FixedUpdate()
     {
          if (target == null) return;

          float distanceToTarget = Vector2.Distance(transform.position, target.position);
          Vector2 directionToTarget = (target.position - transform.position).normalized;

          // --- Priest is within detection range ---
          if (distanceToTarget <= detectionRange)
          {
               returningHome = false; // stop returning if target re-enters range

               if (distanceToTarget > attackRange)
               {
                    // --- Move toward priest ---
                    rb.MovePosition(rb.position + directionToTarget * moveSpeed * Time.fixedDeltaTime);

                    UpdateAnimator(directionToTarget, directionToTarget.sqrMagnitude);
               }
               else
               {
                    // --- Attack priest ---
                    if (Time.time >= lastAttackTime + attackCooldown)
                    {
                         lastAttackTime = Time.time;

                         if (animator != null)
                         {
                              animator.SetFloat("MoveX", directionToTarget.x);
                              animator.SetFloat("MoveY", directionToTarget.y);
                              animator.SetTrigger("Attack");
                         }

                         // TODO: connect to PlayerHealth script
                         // target.GetComponent<PlayerHealth>()?.TakeDamage(10);
                    }

                    StopMoving();
               }
          }
          else
          {
               // --- Priest is too far, return to home position ---
               float distanceToHome = Vector2.Distance(transform.position, homePosition);

               if (distanceToHome > 0.1f)
               {
                    returningHome = true;
                    Vector2 directionToHome = (homePosition - rb.position).normalized;
                    rb.MovePosition(rb.position + directionToHome * moveSpeed * Time.fixedDeltaTime);
                    UpdateAnimator(directionToHome, directionToHome.sqrMagnitude);
               }
               else
               {
                    // Reached home → idle
                    returningHome = false;
                    StopMoving();
               }
          }
     }

     // --- Animator helper ---
     private void UpdateAnimator(Vector2 direction, float speed)
     {
          if (animator == null) return;

          animator.SetFloat("MoveX", direction.x);
          animator.SetFloat("MoveY", direction.y);

          if (HasParameter(animator, "Speed"))
               animator.SetFloat("Speed", speed);
     }

     // --- Stops motion cleanly and sets idle ---
     private void StopMoving()
     {
          rb.linearVelocity = Vector2.zero;

          if (animator != null && HasParameter(animator, "Speed"))
               animator.SetFloat("Speed", 0);
     }

     // --- Checks if animator parameter exists ---
     private bool HasParameter(Animator animator, string paramName)
     {
          foreach (AnimatorControllerParameter param in animator.parameters)
          {
               if (param.name == paramName)
                    return true;
          }
          return false;
     }
}

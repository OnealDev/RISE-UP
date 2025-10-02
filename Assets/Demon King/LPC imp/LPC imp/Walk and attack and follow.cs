using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
     [Header("Target Settings")]
     public Transform target; // Priest to attack

     [Header("Movement Settings")]
     public float moveSpeed = 2f;
     public float stoppingDistance = 1.5f;

     [Header("Attack Settings")]
     public float attackRange = 1.5f;
     public float attackCooldown = 2f;
     private float lastAttackTime;

     private Rigidbody2D rb;
     private Animator animator;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();
     }

     void FixedUpdate()
     {
          if (target == null) return;

          float distance = Vector2.Distance(transform.position, target.position);
          Vector2 direction = (target.position - transform.position).normalized;

          if (distance > attackRange)
          {
               // --- Move towards priest ---
               rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

               if (animator != null)
               {
                    animator.SetFloat("MoveX", direction.x);
                    animator.SetFloat("MoveY", direction.y);

                    // only use Speed if parameter exists in Animator
                    if (HasParameter(animator, "Speed"))
                    {
                         animator.SetFloat("Speed", direction.sqrMagnitude);
                    }
               }
          }
          else
          {
               // --- Attack priest if cooldown allows ---
               if (Time.time >= lastAttackTime + attackCooldown)
               {
                    lastAttackTime = Time.time;

                    if (animator != null)
                    {
                         // Keep facing priest
                         animator.SetFloat("MoveX", direction.x);
                         animator.SetFloat("MoveY", direction.y);

                         animator.SetTrigger("Attack");
                    }

                    // TODO: Call priest’s health script here
                    // target.GetComponent<PlayerHealth>()?.TakeDamage(10);
               }

               // Stop movement cleanly
               rb.linearVelocity = Vector2.zero;

               if (animator != null && HasParameter(animator, "Speed"))
               {
                    animator.SetFloat("Speed", 0);
               }
          }
     }

     // Utility to check if Animator has a parameter
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

using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
     public Transform target;       // The player (LavaSlime)
     public float moveSpeed = 2f;   // Speed of Demon King
     public float stoppingDistance = 1f; // How close before stopping

     private Rigidbody2D rb;
     private Animator animator;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();
     }

     void Update()
     {
          if (target == null) return;

          // Distance between enemy and target
          float distance = Vector2.Distance(transform.position, target.position);

          if (distance > stoppingDistance)
          {
               // Move towards target
               Vector2 direction = (target.position - transform.position).normalized;
               rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

               // Update Animator (optional: set to walk animation)
               animator.SetBool("isWalking", true);
               animator.SetFloat("MoveX", direction.x);
               animator.SetFloat("MoveY", direction.y);
          }
          else
          {
               // Stop moving
               animator.SetBool("isWalking", false);
          }
     }
}


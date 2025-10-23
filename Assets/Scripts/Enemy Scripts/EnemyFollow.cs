using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
     public Transform followTarget;    // Priest to follow
     public float followSpeed = 2f;    // Slime speed
     public float detectionRange = 5f; // Distance before chasing starts

     private Rigidbody2D rb2D;
     private Animator anim;
     private EnemyHealth enemyHealth;
     private Vector2 lastIdleDir = Vector2.down; //  remember last facing direction

     void Start()
     {
          rb2D = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          enemyHealth = GetComponent<EnemyHealth>();
     }

     void FixedUpdate()
     {
          if (followTarget == null) return;

          float distanceToTarget = Vector2.Distance(transform.position, followTarget.position);

          if (distanceToTarget <= detectionRange)
          {
               // Chase priest
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

               // store last direction for idle pose later
               if (direction.sqrMagnitude > 0.01f)
                    lastIdleDir = direction;
          }
          else
          {
               // Priest too far → idle mode
               rb2D.linearVelocity = Vector2.zero;

               if (anim != null)
               {
                    anim.SetFloat("Speed", 0);

                    // keep last facing direction (so idle anim faces last move)
                    anim.SetFloat("MoveX", lastIdleDir.x);
                    anim.SetFloat("MoveY", lastIdleDir.y);
               }
          }
     }
}

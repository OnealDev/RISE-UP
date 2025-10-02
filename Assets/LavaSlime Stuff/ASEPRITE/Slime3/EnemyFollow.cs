using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
     public Transform followTarget;    // Priest to follow
     public float followSpeed = 2f;    // Slime speed
     private Rigidbody2D rb2D;
     private Animator anim;
     private EnemyHealth enemyHealth;

     void Start()
     {
          rb2D = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          enemyHealth = GetComponent<EnemyHealth>();
     }

     void FixedUpdate()
     {
          if (followTarget == null) return;

          // Direction toward target
          Vector2 direction = (followTarget.position - transform.position).normalized;

          // Save last move direction for EnemyHealth (death anim)
          if (enemyHealth != null)
          {
               enemyHealth.lastMoveDir = direction;
          }

          // Move toward priest
          rb2D.MovePosition(rb2D.position + direction * followSpeed * Time.fixedDeltaTime);

          // Update animator (if slime has animations)
          if (anim != null)
          {
               anim.SetFloat("MoveX", direction.x);
               anim.SetFloat("MoveY", direction.y);
               anim.SetFloat("Speed", direction.sqrMagnitude);
          }
     }
}

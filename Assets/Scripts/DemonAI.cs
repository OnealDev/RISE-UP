using UnityEngine;

public class DemonAI : MonoBehaviour
{
     public Transform player;
     public float moveSpeed = 3f;

     [Header("Ranges")]
     public float detectionRange = 5f;
     public float attackRange = 1.5f;

     [Header("Attack Settings")]
     public float punchCooldown = 2f;
     private float lastPunchTime = -999f;

     private Animator anim;
     private Rigidbody2D rb;

     public GameObject hitbox;       // Assign in Inspector
     public GameObject flameEffect;  // Assign in Inspector

     void Start()
     {
          anim = GetComponent<Animator>();
          rb = GetComponent<Rigidbody2D>();

          if (hitbox != null)
               hitbox.SetActive(false);

          if (flameEffect != null)
               flameEffect.SetActive(false);
     }

     void Update()
     {
          if (anim.GetBool("isDead")) return;

          float distance = Vector2.Distance(transform.position, player.position);

          if (distance <= detectionRange)
          {
               if (distance > attackRange)
               {
                    ChasePlayer();
               }
               else
               {
                    TryPunch();
               }
          }
          else
          {
               rb.linearVelocity = Vector2.zero;
               anim.SetFloat("Speed", 0f);
          }
     }

     private void ChasePlayer()
     {
          Vector2 direction = (player.position - transform.position).normalized;
          rb.linearVelocity = direction * moveSpeed;

          anim.SetFloat("MoveX", direction.x);
          anim.SetFloat("MoveY", direction.y);
          anim.SetFloat("Speed", moveSpeed);
     }

     private void TryPunch()
     {
          rb.linearVelocity = Vector2.zero;

          if (Time.time - lastPunchTime >= punchCooldown)
          {
               anim.SetTrigger("PunchTrigger");
               lastPunchTime = Time.time;
          }
     }

     public void EnableHitbox()
     {
          if (hitbox != null)
               hitbox.SetActive(true);
     }

     public void DisableHitbox()
     {
          if (hitbox != null)
               hitbox.SetActive(false);
     }

     public void ActivateFlame()
     {
          if (flameEffect != null)
               flameEffect.SetActive(true);
     }

     public void DeactivateFlame()
     {
          if (flameEffect != null)
               flameEffect.SetActive(false);
     }

     public void Die()
     {
          anim.SetBool("isDead", true);
          rb.linearVelocity = Vector2.zero;
          this.enabled = false;
     }
}

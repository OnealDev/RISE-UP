using UnityEngine;
using System.Collections;

public class DemonAI : MonoBehaviour
{
     [Header("Player Targeting")]
     public Transform player;
     public float moveSpeed = 2f;
     public float detectionRange = 6f;
     public float attackRange = 2.5f;

     [Header("Attack Settings")]
     public GameObject flameEffect;
     public GameObject hitbox;
     public float attackCooldown = 2f;

     private Animator anim;
     private SpriteRenderer sprite;
     private Rigidbody2D rb;

     private DemonKnockback knockback;
     private bool facingRight = true;
     private bool isAttacking = false;
     private bool isDead = false;
     private float lastAttackTime;

     void Awake()
     {
          anim = GetComponent<Animator>();
          sprite = GetComponent<SpriteRenderer>();
          rb = GetComponent<Rigidbody2D>();
          knockback = GetComponent<DemonKnockback>();
     }

     void Start()
     {
          if (player == null)
          {
               GameObject p = GameObject.FindGameObjectWithTag("Player");
               if (p != null)
                    player = p.transform;
          }

          if (hitbox != null)
               hitbox.SetActive(false);

          if (flameEffect != null)
               flameEffect.SetActive(false);
     }

     void FixedUpdate()
     {
          if (isDead || player == null) return;

          if (knockback != null && knockback.IsKnocked)
          {
               rb.linearVelocity = Vector2.zero;
               anim.SetBool("isMoving", false);
               anim.SetBool("isAttacking", false);
               return;
          }

          float distance = Vector2.Distance(transform.position, player.position);

          bool shouldFaceRight = player.position.x > transform.position.x;
          if (shouldFaceRight != facingRight)
               Flip();

          if (distance <= detectionRange && distance > attackRange)
          {
               anim.SetBool("isMoving", true);
               MoveTowardPlayer();
          }
          else
          {
               anim.SetBool("isMoving", false);
               rb.linearVelocity = Vector2.zero;
          }

          if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
          {
               StartCoroutine(AttackSequence());
          }
     }

     void MoveTowardPlayer()
     {
          if (player == null) return;

          Vector2 dir = (player.position - transform.position).normalized;
          rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
     }

     IEnumerator AttackSequence()
     {
          if (isAttacking || isDead) yield break;

          isAttacking = true;
          lastAttackTime = Time.time;

          anim.SetBool("isAttacking", true);

          yield return new WaitForSeconds(0.25f);

          if (hitbox != null) hitbox.SetActive(true);
          if (flameEffect != null) flameEffect.SetActive(true);

          yield return new WaitForSeconds(0.25f);

          if (hitbox != null) hitbox.SetActive(false);
          if (flameEffect != null) flameEffect.SetActive(false);

          anim.SetBool("isAttacking", false);

          yield return new WaitForSeconds(attackCooldown - 0.25f);

          isAttacking = false;
     }

     void Flip()
     {
          facingRight = !facingRight;
          sprite.flipX = !sprite.flipX;

          // Flip flame and hitbox local position
          if (flameEffect != null)
          {
               Vector3 pos = flameEffect.transform.localPosition;
               pos.x *= -1;
               flameEffect.transform.localPosition = pos;

               // Update flame swipe direction
               FlameAnimator flameAnim = flameEffect.GetComponent<FlameAnimator>();
               if (flameAnim != null)
                    flameAnim.facingRight = facingRight;
          }

          if (hitbox != null)
          {
               Vector3 pos = hitbox.transform.localPosition;
               pos.x *= -1;
               hitbox.transform.localPosition = pos;
          }
     }

     public void Die()
     {
          isDead = true;

          anim.SetBool("isDead", true);
          anim.SetTrigger("Die");

          rb.linearVelocity = Vector2.zero;

          if (hitbox != null) hitbox.SetActive(false);
          if (flameEffect != null) flameEffect.SetActive(false);

          foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
               col.enabled = false;

          StopAllCoroutines();

          Destroy(gameObject, 2.5f);
     }
}

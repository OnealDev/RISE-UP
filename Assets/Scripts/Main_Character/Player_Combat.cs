using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player_Combat : MonoBehaviour
{
     [Header("Combat Settings")]
     public float weaponRange = 1;
     public LayerMask enemyLayer;
     public int damage = 1;

     [Header("Attack Points")]
     public Transform attackPointUp;
     public Transform attackPointDown;
     public Transform attackPointLeft;
     public Transform attackPointRight;
     public float knockbackForce = 50;

     [Header("References")]
     public Animator animator;
     private float lastAttackTime;
     [SerializeField] private float attackCooldown = 0.5f;
     private Vector2 lastMoveDir = Vector2.down;

     //Adding a "combo system" (it just keeps track of which attack to do)
     private int currentAttack = 0;
     [SerializeField] private int maxCombo = 2;
     [SerializeField] private float comboResetTime = 1f;
     
     // NEW: Prevent multiple damage calls
     private bool hasDealtDamageThisAttack = false;

     private void Awake()
     {
          animator = GetComponent<Animator>();
     }

     public void OnAttack(InputAction.CallbackContext context) //Called when attack action fires by the playerinput
     {
          //Attack cooldown
          if (Time.time - lastAttackTime < attackCooldown)
               return;

          lastAttackTime = Time.time;

          // NEW: Reset damage flag for new attack
          hasDealtDamageThisAttack = false;

          currentAttack++; //increment combo
          if (currentAttack > maxCombo)
               currentAttack = 1;

          animator.SetFloat("AttackIndex", currentAttack);

          animator.SetTrigger("AttackTrigger"); //Triggers attack animation

          //Lock player in the direction of the attack
          animator.SetFloat("InputX", lastMoveDir.x);
          animator.SetFloat("InputY", lastMoveDir.y);

          //reset the combo after some time
          CancelInvoke(nameof(ResetCombo));
          Invoke(nameof(ResetCombo), comboResetTime);
     }

     public void SetFacingDirection(Vector2 dir) //updates facing direction
     {
          if (dir.sqrMagnitude > 0.01f)
               lastMoveDir = dir.normalized;
     }

     public void DealDamage()
{
     
     if (hasDealtDamageThisAttack) return;
     hasDealtDamageThisAttack = true;

     Transform activePoint = GetActiveAttackPoint();
     if (activePoint == null)
          return;

     Collider2D[] enemies = Physics2D.OverlapCircleAll(activePoint.position, weaponRange, enemyLayer);

     foreach (Collider2D enemy in enemies)
     {
          
          Vector2 hitPoint = enemy.ClosestPoint(activePoint.position);
          Vector2 attackDirection = (enemy.transform.position - transform.position).normalized;

          //Apply Damage and knockbac
          enemy.GetComponent<EnemyHealth>()?.ChangeHealth(-damage);
          enemy.GetComponent<Enemy_Knockback>()?.Knockback(transform, knockbackForce, 0.2f, 0.5f);

          // Neal NEW ADDITION: Damage breakable rocks
          if (enemy.CompareTag("Rock"))
          {
               enemy.GetComponent<BreakableRock>()?.TakeDamage(damage);
          }

          //Spawn particles
          HitEffectManager.Instance?.SpawnHitEffect(hitPoint, attackDirection);

          //screen shake
          ScreenShake.Instance?.QuickShake();
     }
}
     private Transform GetActiveAttackPoint()
     {
          if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
          {
               // Horizontal attack
               return lastMoveDir.x > 0 ? attackPointRight : attackPointLeft;
          }
          else
          {
               // Vertical attack
               return lastMoveDir.y > 0 ? attackPointUp : attackPointDown;
          }
     }

     public void FinishAttacking() //At the end of the attack
     {
          //causes the trigger to auto reset
     }

     private void ResetCombo()
     {
          currentAttack = 0;
     }

     private void OnDrawGizmosSelected()
     {
          Gizmos.color = Color.yellow;

          if (attackPointUp) Gizmos.DrawWireSphere(attackPointUp.position, weaponRange);
          if (attackPointDown) Gizmos.DrawWireSphere(attackPointDown.position, weaponRange);
          if (attackPointLeft) Gizmos.DrawWireSphere(attackPointLeft.position, weaponRange);
          if (attackPointRight) Gizmos.DrawWireSphere(attackPointRight.position, weaponRange);
     }
}
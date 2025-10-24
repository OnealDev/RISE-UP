using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class AryasPlayerMovement : MonoBehaviour
{
     public float speed = 5; //how fast player moves 
     public int facingDirection = 5; //controls direction sprite is facing
     public Rigidbody2D rb; //Must communicate with rigid body
     public Animator anim;

     public Player_Combat player_Combat;

     // Attack cooldown variables
     [Header("Attack Cooldown Settings")]
     public float attackCooldown = 0.5f;     // how long to wait between attacks
     private float lastAttackTime;           // stores the last time an attack was made

     private void Update()
     {
          // Attack input with cooldown check
          if (Input.GetButtonDown("Horizontal Attack") && Time.time >= lastAttackTime + attackCooldown)
          {
               lastAttackTime = Time.time; // reset timer
               player_Combat.Attack();     // trigger attack
          }
     }
  
     //Dashing 
     private bool canDash = true;
     private bool isDashing;
     private float dashingPower = 24f;
     private float dashingTime = 0.2f;
     private float dashingCooldown = 1f;

     [SerializeField] private TrailRenderer tr;
     // Update is called 50x per frame
     void FixedUpdate()
     {
          float horizontal = Input.GetAxis("Horizontal");
          float vertical = Input.GetAxis("Vertical");

          // Set facing direction for attack logic
          if (horizontal != 0)
               facingDirection = 1;  // side (used for both left/right)
          else if (vertical > 0)
               facingDirection = 2;  // up
          else if (vertical < 0)
               facingDirection = 3;  // down

          if (isDashing)
          {
               return;
          }
          if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
          {
               Flip();
          }
          anim.SetFloat("horizontal", horizontal);
          anim.SetFloat("vertical", vertical);

          rb.linearVelocity = new Vector2(horizontal, vertical) * speed;

          if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
          {
               StartCoroutine(Dash());
          }
     }

     void Flip()
     {
          facingDirection *= -1;
          transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
     }

     private IEnumerator Dash()
     {
          canDash = false;
          isDashing = true;
          float originalGravity = rb.gravityScale;
          rb.gravityScale = 0f;
          rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
          tr.emitting = true;
          yield return new WaitForSeconds(dashingTime);
          tr.emitting = false;
          rb.gravityScale = originalGravity;
          isDashing = false;
          yield return new WaitForSeconds(dashingCooldown);
          canDash = true;
     }
}


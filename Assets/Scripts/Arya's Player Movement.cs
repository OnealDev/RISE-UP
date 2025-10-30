using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine.InputSystem;

public class AryasPlayerMovement : MonoBehaviour
{
     [SerializeField] public float moveSpeed = 5; //how fast player moves 
     public Rigidbody2D rb; //Must communicate with rigid body
     private Vector2 moveInput;
     public int facingDirection = 5; //controls direction sprite is facing

     public Animator animator;

     public Player_Combat player_Combat;

     // Attack cooldown variables
     [Header("Attack Cooldown Settings")]
     public float attackCooldown = 0.5f;     // how long to wait between attacks
     private float lastAttackTime;           // stores the last time an attack was made

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();
     }
     private void Update()
     {
          rb.linearVelocity = moveInput * moveSpeed;
          // Attack input with cooldown check
          if (Input.GetButtonDown("Horizontal Attack") && Time.time >= lastAttackTime + attackCooldown)
          {
               lastAttackTime = Time.time; // reset timer
               player_Combat.Attack();     // trigger attack
          }
     }

   
     public void Move(InputAction.CallbackContext context)
     {
          animator.SetBool("IsWalking", true);

          if (context.canceled)
          {
               animator.SetBool("IsWalking", false);
               animator.SetFloat("LastInputX", moveInput.x);
               animator.SetFloat("LastInputY", moveInput.y);

          }
        
          moveInput = context.ReadValue<Vector2>();
          animator.SetFloat("InputX", moveInput.x);
          animator.SetFloat("InputY", moveInput.y);

     }

    

     
     /* private IEnumerator Dash()
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
  //Dashing 
     private bool canDash = true;
     private bool isDashing;
     private float dashingPower = 24f;
     private float dashingTime = 0.2f;
     private float dashingCooldown = 1f;

     [SerializeField] private TrailRenderer tr;
     */

}


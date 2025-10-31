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
          player_Combat.SetFacingDirection(moveInput);
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
}


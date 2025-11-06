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
     //Neal's Changes:
     public float strength = 1f;         // Just keeps track of how powerful the player is
     public float massGainPerBible = 0.5f; // How much mass to add per pickup 


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

          //O'Neal's changes to pick up item
          if (Keyboard.current.eKey.wasPressedThisFrame)
          {
               TryInteract();
          }

     }
     //O'Neal's changes to pick up item
     void TryInteract()
     {
          Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);
          foreach (Collider2D hit in hits)
          {
               IInteractable interactable = hit.GetComponent<IInteractable>();
               if (interactable != null && interactable.CanInteract())
               {
                    interactable.Interact();
                    break;
               }
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
}


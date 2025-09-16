using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     public float moveSpeed = 5f;              // Movement speed
     private Rigidbody2D rb;                   // Reference to Rigidbody2D
     private Animator animator;                // Reference to Animator
     private Vector2 movement;                 // Current input
     private Vector2 lastMoveDirection = Vector2.down; // Default facing down

     [Header("Physics Settings")]
     public bool lockZRotation = true;         // Option to freeze Z rotation

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();

          // Lock Z rotation if enabled
          if (lockZRotation)
          {
               rb.freezeRotation = true; // Same as checking "Freeze Rotation Z" in Inspector
          }
     }

     void Update()
     {
          // --- Get input (arrow keys or WASD) ---
          movement.x = Input.GetAxisRaw("Horizontal"); // Left (-1), Right (1)
          movement.y = Input.GetAxisRaw("Vertical");   // Down (-1), Up (1)

          // --- Animator parameters ---
          animator.SetFloat("MoveX", movement.x);
          animator.SetFloat("MoveY", movement.y);
          animator.SetFloat("Speed", movement.sqrMagnitude);

          // --- Save last direction when moving ---
          if (movement != Vector2.zero)
          {
               lastMoveDirection = movement;
          }

          // --- Pass last idle direction to Animator ---
          animator.SetFloat("LastMoveX", lastMoveDirection.x);
          animator.SetFloat("LastMoveY", lastMoveDirection.y);
     }

     void FixedUpdate()
     {
          // --- Apply movement ---
          rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
     }
}

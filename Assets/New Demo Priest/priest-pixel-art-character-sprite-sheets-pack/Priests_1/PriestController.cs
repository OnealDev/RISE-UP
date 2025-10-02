using UnityEngine;

public class PriestController : MonoBehaviour
{
     [Header("Movement Settings")]
     public float moveSpeed = 5f;

     [Header("Attack Settings")]
     public float attackCooldown = 0.5f; // time between attacks
     private float lastAttackTime;

     [Header("References")]
     public GameObject attackHitbox; // assign in Inspector

     private Rigidbody2D rb;
     private Animator animator;
     private Vector2 movement;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          animator = GetComponent<Animator>();

          if (attackHitbox != null)
          {
               attackHitbox.SetActive(false); // keep disabled until attacking
          }
     }

     void Update()
     {
          // Input works with both WASD and Arrow Keys by default
          movement.x = Input.GetAxisRaw("Horizontal");
          movement.y = Input.GetAxisRaw("Vertical");

          // Update walking state in Animator
          animator.SetBool("isWalking", movement.sqrMagnitude > 0);

          // Attack input (Spacebar) with cooldown
          if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
          {
               lastAttackTime = Time.time; // reset cooldown
               animator.SetTrigger("Attack"); // animation will call EnableHitbox/DisableHitbox
          }
     }

     void FixedUpdate()
     {
          // Apply movement
          rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
     }

     // Called by Animation Events in your Attack animation
     public void EnableHitbox()
     {
          if (attackHitbox != null)
               attackHitbox.SetActive(true);
     }

     public void DisableHitbox()
     {
          if (attackHitbox != null)
               attackHitbox.SetActive(false);
     }
}

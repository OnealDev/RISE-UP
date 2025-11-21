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
    


    //Dash Variables 
    [Header("Dash Settings")]
    public float dashSpeed = 25f; // How fast the dash moves the player
    public float dashDuration = 0.2f; // How long the dash lasts
    public int maxDashes = 2;
    public float dashResetTime = 0.25f; // Allows chaining dashes quickly
    public LayerMask obstacleLayerMask = 1;

    //Neal's Changes:
    public float strength = 1f;         // Just keeps track of how powerful the player is
    public float massGainPerBible = 0.5f;

     public Player_Combat player_Combat;

    // Attack cooldown variables
    [Header("Attack Cooldown Settings")]
    public float attackCooldown = 0.5f;     // how long to wait between attacks
    private float lastAttackTime;           // stores the last time an attack was made

    //Dash State Variables 
    private int currentDashes;
    private float lastDashTime;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private Vector2 dashDirection;
    private TrailRenderer trailRenderer; // Already assigned in Inspector
    private Vector2 lastNonZeroMoveInput;
     //Neal
     private PlayerFallEffect fallEffect;

     public bool isShooting;

     void Start()
    {
         
          rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentDashes = maxDashes;

        // Get the TrailRenderer from Inspector
        trailRenderer = GetComponent<TrailRenderer>();
        if(trailRenderer == null)
            Debug.LogWarning("TrailRenderer missing! Add it in the Inspector.");

        trailRenderer.emitting = false;

        // Rigidbody2D setup for top-down movement
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


         // Neal
          fallEffect = GetComponent<PlayerFallEffect>();


     }

     private void Update()
{
    //stop movement while firing
    if (isShooting)
    {
        rb.linearVelocity = Vector2.zero;
        return; 
    }

    // store last nonzero movement
    if (moveInput != Vector2.zero)
        lastNonZeroMoveInput = moveInput;

    // facing direction
    player_Combat.SetFacingDirection(moveInput);

    // dash reset
    if (currentDashes < maxDashes && Time.time - lastDashTime >= dashResetTime)
        currentDashes = maxDashes;

    // dash duration
    if (isDashing)
    {
        dashTimeLeft -= Time.deltaTime;
        if (dashTimeLeft <= 0)
            EndDash();
    }

    //item interaction
    if (Keyboard.current.eKey.wasPressedThisFrame)
        TryInteract();
}

    //Neals changes to try interact
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
        moveInput = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        // Walking animation
        if(context.performed)
            animator.SetBool("IsWalking", true);
        else if(context.canceled)
            animator.SetBool("IsWalking", false);

        // Update last non-zero input for dash
        if(moveInput != Vector2.zero)
            lastNonZeroMoveInput = moveInput;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!context.performed || currentDashes <= 0 || isDashing)
            return;

        // Use last input direction; default to up if idle
        dashDirection = lastNonZeroMoveInput != Vector2.zero ? lastNonZeroMoveInput.normalized : Vector2.up;

        // Check if dash path is clear
        if(IsDashPathClear())
        {
            StartDash();
        }
    }

    bool IsDashPathClear()
    {
        Vector2 checkDirection = dashDirection;
        float dashDistance = dashSpeed * dashDuration;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDirection, dashDistance, obstacleLayerMask);

        if (hit.collider != null)
            Debug.Log("DASH BLOCKED BY: " + hit.collider.name);
        else
            Debug.Log("DASH PATH CLEAR");

        return !hit.collider;
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;

        // Use one dash charge
        currentDashes--;
        lastDashTime = Time.time;

        // Start trail effect
        if(trailRenderer != null)
        {
            trailRenderer.Clear();
            trailRenderer.time = dashDuration * 1.2f;
            trailRenderer.emitting = true;
        }

        Debug.Log($"Dashing! Dashes remaining: {currentDashes}");
    }

    void EndDash()
    {
        isDashing = false;
        if(trailRenderer != null)
            trailRenderer.emitting = false;
    }

    void FixedUpdate()
    {

         // Neal: Lock movement during fall
         if (fallEffect != null && fallEffect.IsFalling()) return;

          // STOP ALL MOVEMENT IF SHOOTING
         if (isShooting)
        {
             rb.linearVelocity = Vector2.zero;
            return;
        }

            if (isDashing)
        {
              rb.linearVelocity = dashDirection * dashSpeed;
        }
        else
        {
               rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}
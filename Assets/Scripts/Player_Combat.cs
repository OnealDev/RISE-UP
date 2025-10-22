using UnityEngine;

public class Player_Combat : MonoBehaviour
{
     [Header("Component References")]
     public Animator anim;
     public AryasPlayerMovement playerMovement; // Reference to Priest's movement script

     public void Attack()
     {
          if (anim == null || playerMovement == null)
          {
               Debug.LogWarning("Missing Animator or PlayerMovement reference!");
               return;
          }

          // Get the Priest's current facing direction (1 = side, 2 = up, 3 = down)
          int direction = playerMovement.facingDirection;

          // Pass that info to the Animator so the right attack animation plays
          anim.SetInteger("FacingDirection", direction);

          // Activate the trigger to start the attack animation
          anim.SetTrigger("AttackTrigger");

          Debug.Log($"Attack triggered in direction: {direction}");
     }

     // Optional: Resets the trigger after the animation ends
     public void FinishAttacking()
     {
          if (anim != null)
               anim.ResetTrigger("AttackTrigger");
     }
}


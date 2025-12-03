using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [Header("Boss Settings")]
    public Animator bossAnimator;
    public string activationTrigger = "Activate";
    public string activationBool = "IsActivated";
    
    [Header("Activation Method")]
    public bool useTriggerZone = true;
    public float activationDistance = 15f;
    
    private Transform player;
    private bool hasActivated = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Make sure boss starts in deactivated state
        if (bossAnimator != null)
        {
            bossAnimator.SetBool(activationBool, false);
        }
    }
    
    void Update()
    {
        // If using distance check and not activated yet
        if (!useTriggerZone && !hasActivated && player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= activationDistance)
            {
                ActivateBoss();
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // If using trigger zone and player enters
        if (useTriggerZone && !hasActivated && other.CompareTag("Player"))
        {
            ActivateBoss();
        }
    }
    
    void ActivateBoss()
    {
        if (bossAnimator == null || hasActivated) return;
        
        hasActivated = true;
        
        // Method 1: Use Trigger for wake-up animation
        bossAnimator.SetTrigger(activationTrigger);
        
        // Method 2: OR directly set bool to transition to Normal state
        bossAnimator.SetBool(activationBool, true);
        
        Debug.Log("BOSS ACTIVATED!");
        
        // Optional: Play activation sound
        // Optional: Screen shake
        // Optional: Show UI text "BOSS BATTLE!"
    }
    
    // Optional: Draw activation range in Scene view
    void OnDrawGizmosSelected()
    {
        if (!useTriggerZone)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, activationDistance);
        }
    }
}
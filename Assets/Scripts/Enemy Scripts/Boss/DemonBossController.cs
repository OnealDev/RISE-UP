using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemonBossController : MonoBehaviour
{
    [System.Serializable]
    public class BossAttack
    {
        public string attackName;
        public float weight = 1f; // Chance weight
        public float minRange = 0f; // Minimum distance to use
        public float maxRange = 10f; // Maximum distance to use
        public float cooldown = 3f;
        public bool isRanged = false;
        
        [HideInInspector] public float lastUsedTime = -999f;
    }
    
    [Header("State Management")]
    public bool isEnraged = false;
    public float enrageHealthPercent = 0.5f;
    
    [Header("Attack Settings")]
    public List<BossAttack> normalAttacks = new List<BossAttack>();
    public List<BossAttack> enragedAttacks = new List<BossAttack>();
    public float timeBetweenAttacks = 2f;
    
    [Header("References")]
    public Animator animator;
    public EnemyHealth enemyHealth;
    public Transform player;// 
    public SpriteRenderer spriteRenderer;
    
    private float nextAttackTime = 0f;
    private bool canAttack = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Initialize attacks - YOU'LL SET THESE IN INSPECTOR!
        // We'll set up default values
        InitializeDefaultAttacks();
    }
    
    void InitializeDefaultAttacks()
    {
        // Normal attacks
        normalAttacks.Add(new BossAttack() { 
            attackName = "CleaverSweep", 
            weight = 40f, 
            maxRange = 3f,
            cooldown = 4f 
        });
        
        normalAttacks.Add(new BossAttack() { 
            attackName = "SpellCast", 
            weight = 25f, 
            minRange = 4f,
            isRanged = true,
            cooldown = 5f 
        });
        
        normalAttacks.Add(new BossAttack() { 
            attackName = "FireBreath", 
            weight = 20f, 
            maxRange = 2.5f,
            cooldown = 6f 
        });
        
        normalAttacks.Add(new BossAttack() { 
            attackName = "ChargeAttack", 
            weight = 15f, 
            maxRange = 6f,
            cooldown = 7f 
        });
        
        // Enraged attacks (add SummoningSlam)
        enragedAttacks.Add(new BossAttack() { 
            attackName = "Enraged_CleaverSweep", 
            weight = 30f, 
            maxRange = 3f,
            cooldown = 3f 
        });
        
        enragedAttacks.Add(new BossAttack() { 
            attackName = "Enraged_SpellCast", 
            weight = 20f, 
            minRange = 4f,
            isRanged = true,
            cooldown = 4f 
        });
        
        enragedAttacks.Add(new BossAttack() { 
            attackName = "Enraged_FireBreath", 
            weight = 20f, 
            maxRange = 2.5f,
            cooldown = 5f 
        });
        
        enragedAttacks.Add(new BossAttack() { 
            attackName = "Enraged_ChargeAttack", 
            weight = 15f, 
            maxRange = 6f,
            cooldown = 5f 
        });
        
        enragedAttacks.Add(new BossAttack() { 
            attackName = "SummoningSlam", 
            weight = 15f, 
            maxRange = 8f,
            cooldown = 8f 
        });
    }
    
    void Update()
    {
        if (player == null) return;
        
        // Flip sprite based on player position
        FlipSpriteToPlayer();
        
        // Check for enrage transition
        CheckEnrage();
        
        // Handle attacks
        if (canAttack && Time.time >= nextAttackTime)
        {
            ChooseAndExecuteAttack();
        }
    }
    
    void FlipSpriteToPlayer()
    {
        if (player.position.x > transform.position.x)
            spriteRenderer.flipX = true;  // Face right
        else
            spriteRenderer.flipX = false; // Face left
    }
    
    void CheckEnrage()
    {
        if (!isEnraged && enemyHealth != null)
        {
            float healthPercent = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
            if (healthPercent <= enrageHealthPercent)
            {
                StartEnrage();
            }
        }
    }
    
    void StartEnrage()
    {
        isEnraged = true;
        animator.SetBool("IsEnraged", true);
        
        // Start invincibility during transition
        StartCoroutine(EnrageTransition());
    }
    
    IEnumerator EnrageTransition()
    {
        // Make boss temporarily invincible
        enemyHealth.isInvincible = true;
        
        // Add any transition effects here
        yield return new WaitForSeconds(2f);
        
        enemyHealth.isInvincible = false;
        canAttack = true;
    }
    
    void ChooseAndExecuteAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        List<BossAttack> availableAttacks = isEnraged ? enragedAttacks : normalAttacks;
        
        // Filter attacks by range and cooldown
        List<BossAttack> validAttacks = new List<BossAttack>();
        float totalWeight = 0f;
        
        foreach (BossAttack attack in availableAttacks)
        {
            if (Time.time - attack.lastUsedTime >= attack.cooldown &&
                distanceToPlayer >= attack.minRange && 
                distanceToPlayer <= attack.maxRange)
            {
                validAttacks.Add(attack);
                totalWeight += attack.weight;
            }
        }
        
        // No valid attacks? Wait a bit
        if (validAttacks.Count == 0)
        {
            nextAttackTime = Time.time + 1f;
            return;
        }
        
        // Weighted random selection
        float randomPoint = Random.Range(0f, totalWeight);
        BossAttack selectedAttack = null;
        
        foreach (BossAttack attack in validAttacks)
        {
            if (randomPoint < attack.weight)
            {
                selectedAttack = attack;
                break;
            }
            randomPoint -= attack.weight;
        }
        
        // Execute the attack
        if (selectedAttack != null)
        {
            ExecuteAttack(selectedAttack);
        }
    }
    
    void ExecuteAttack(BossAttack attack)
    {
        Debug.Log($"Executing attack: {attack.attackName}");
        
        // Update cooldown
        attack.lastUsedTime = Time.time;
        nextAttackTime = Time.time + timeBetweenAttacks;
        
        // Trigger the attack in Animator
        animator.SetTrigger("AttackTrigger");
        
        // We'll need a way to tell WHICH attack was triggered
        // We can use an Animator parameter or direct animation crossfade
        // For now, let's use a simple method - we'll improve this
        
        // TEMPORARY: Use crossfade to play specific attack
        animator.CrossFade(attack.attackName, 0.1f);
    }
    
    // Call this when boss takes damage to sync health
    public void OnDamageTaken()
    {
        // Optional: React to damage (like interrupting attacks)
    }
}
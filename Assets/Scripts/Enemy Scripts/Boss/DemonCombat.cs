using UnityEngine;
using System.Collections;

public class DemonCombat : MonoBehaviour
{
    [Header("Damage Values")]
    public int cleaverDamage = 2;
    public int spellDamage = 1;
    public int fireBreathDamage = 1;
    public int chargeDamage = 2;
    public int slamDamage = 3;
    
    [Header("Attack Hitboxes")]
    public Transform cleaverHitbox;
    public Transform fireBreathHitbox;
    public Transform chargeHitbox;
    
    [Header("Fire Breath Settings")]
    public float fireBreathSweepDuration = 1.0f;
    public float fireBreathTopHeight = 1.5f;
    public float fireBreathBottomHeight = -1.0f;
    private bool isFireBreathActive = false;
    
    [Header("Projectile Settings")]
    public GameObject fireballPrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;
    
    [Header("Summon Settings")]
    public GameObject impPrefab;
    public int impsToSpawn = 3;
    
    private EnemyHealth enemyHealth;
    
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        
        // Disable all hitboxes initially
        if (cleaverHitbox != null) cleaverHitbox.gameObject.SetActive(false);
        if (fireBreathHitbox != null) fireBreathHitbox.gameObject.SetActive(false);
        if (chargeHitbox != null) chargeHitbox.gameObject.SetActive(false);
    }
    
    void Update()
    {
        // DEBUG: Draw hitbox outlines in Game view
        DrawDebugHitboxes();
    }
    
    // ===== HITBOX CONTROL METHODS =====
    public void EnableCleaverHitbox()
    {
        if (cleaverHitbox != null)
        {
            cleaverHitbox.gameObject.SetActive(true);
            Debug.Log("Cleaver hitbox ENABLED");
        }
    }
    
    public void DisableCleaverHitbox()
    {
        if (cleaverHitbox != null)
        {
            cleaverHitbox.gameObject.SetActive(false);
        }
    }
    
    // FIRE BREATH SWEEP METHODS
    public void StartFireBreathSweep()
    {
        if (fireBreathHitbox != null)
        {
            fireBreathHitbox.gameObject.SetActive(true);
            isFireBreathActive = true;
            
            // Start at top position
            Vector3 startPos = fireBreathHitbox.localPosition;
            startPos.y = fireBreathTopHeight;
            fireBreathHitbox.localPosition = startPos;
            
            // Start sweeping coroutine
            StartCoroutine(SweepFireBreathDown());
            
            Debug.Log("Fire Breath sweep STARTED");
        }
    }
    
    private IEnumerator SweepFireBreathDown()
    {
        float elapsed = 0f;
        
        Vector3 startPos = fireBreathHitbox.localPosition;
        Vector3 endPos = startPos;
        endPos.y = fireBreathBottomHeight;
        
        while (elapsed < fireBreathSweepDuration && isFireBreathActive)
        {
            // Smoothly move hitbox from top to bottom
            float t = elapsed / fireBreathSweepDuration;
            fireBreathHitbox.localPosition = Vector3.Lerp(startPos, endPos, t);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Ensure hitbox ends at bottom position
        if (fireBreathHitbox != null)
        {
            fireBreathHitbox.localPosition = endPos;
        }
    }
    
    public void DisableFireBreathHitbox()
    {
        if (fireBreathHitbox != null)
        {
            isFireBreathActive = false;
            fireBreathHitbox.gameObject.SetActive(false);
            Debug.Log("Fire Breath sweep ENDED");
        }
    }
    
    public void EnableChargeHitbox()
    {
        if (chargeHitbox != null)
        {
            chargeHitbox.gameObject.SetActive(true);
        }
    }
    
    public void DisableChargeHitbox()
    {
        if (chargeHitbox != null)
        {
            chargeHitbox.gameObject.SetActive(false);
        }
    }
    
    // ===== SPECIAL ATTACK METHODS =====
    public void CastSpell()
    {
        if (fireballPrefab != null && projectileSpawnPoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, 
                projectileSpawnPoint.position, 
                Quaternion.identity);
            
            // TODO: Aim at player (we'll implement this next)
            Debug.Log("Spell cast! (Fireball created)");
        }
    }
    
    public void PerformSummoningSlam()
    {
        // Screen shake
        if (ScreenShake.Instance != null)
            ScreenShake.Instance.BigShake();
        
        // Spawn imps around boss
        for (int i = 0; i < impsToSpawn; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + 
                Random.insideUnitCircle * 3f;
            
            if (impPrefab != null)
                Instantiate(impPrefab, spawnPos, Quaternion.identity);
        }
        
        Debug.Log("Summoning Slam! Imps spawned.");
    }
    
    // ===== HIT DETECTION =====
    public void OnHitboxTrigger(Collider2D collision, string attackType)
    {
        if (collision.CompareTag("Player"))
        {
            int damage = 0;
            
            switch (attackType)
            {
                case "Cleaver": damage = cleaverDamage; break;
                case "FireBreath": damage = fireBreathDamage; break;
                case "Charge": damage = chargeDamage; break;
            }
            
            if (damage > 0)
            {
                DealDamage(collision.transform, damage);
            }
        }
    }
    
    private void DealDamage(Transform target, int damage)
    {
        HealthManager playerHealth = target.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            Vector2 knockbackDirection = (target.position - transform.position).normalized;
            playerHealth.TakeDamage(damage, knockbackDirection);
            Debug.Log($"Dealt {damage} damage to player!");
        }
    }
    
    // ===== DEBUG VISUALIZATION =====
    void DrawDebugHitboxes()
    {
        // Draw active hitboxes in Game view
        if (cleaverHitbox != null && cleaverHitbox.gameObject.activeSelf)
            DrawBoxOutline(cleaverHitbox.GetComponent<BoxCollider2D>().bounds, Color.yellow);
        
        if (fireBreathHitbox != null && fireBreathHitbox.gameObject.activeSelf)
            DrawBoxOutline(fireBreathHitbox.GetComponent<BoxCollider2D>().bounds, Color.red);
        
        if (chargeHitbox != null && chargeHitbox.gameObject.activeSelf)
            DrawBoxOutline(chargeHitbox.GetComponent<BoxCollider2D>().bounds, Color.cyan);
    }
    
    void DrawBoxOutline(Bounds bounds, Color color)
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        
        Debug.DrawLine(new Vector3(min.x, min.y), new Vector3(max.x, min.y), color);
        Debug.DrawLine(new Vector3(max.x, min.y), new Vector3(max.x, max.y), color);
        Debug.DrawLine(new Vector3(max.x, max.y), new Vector3(min.x, max.y), color);
        Debug.DrawLine(new Vector3(min.x, max.y), new Vector3(min.x, min.y), color);
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw fire breath sweep area (in Scene view)
        if (fireBreathHitbox != null)
        {
            Gizmos.color = Color.red;
            
            // Get world positions for top and bottom of sweep
            Vector3 topPos = transform.position;
            topPos.y += fireBreathTopHeight;
            
            Vector3 bottomPos = transform.position;
            bottomPos.y += fireBreathBottomHeight;
            
            // Draw sweep path
            Gizmos.DrawLine(topPos, bottomPos);
            
            // Draw arrows along sweep path
            float arrowSpacing = 0.3f;
            for (float y = fireBreathTopHeight; y > fireBreathBottomHeight; y -= arrowSpacing)
            {
                Vector3 arrowPos = transform.position + Vector3.up * y;
                Gizmos.DrawWireSphere(arrowPos, 0.1f);
            }
            
            // Draw preview area
            Vector3 center = transform.position + Vector3.up * ((fireBreathTopHeight + fireBreathBottomHeight) / 2);
            Vector3 size = new Vector3(2f, fireBreathTopHeight - fireBreathBottomHeight, 0f);
            Gizmos.DrawWireCube(center, size);
        }
        
        // Draw cleaver hitbox preview
        if (cleaverHitbox != null)
        {
            Gizmos.color = Color.yellow;
            if (Application.isPlaying && cleaverHitbox.gameObject.activeSelf)
            {
                Gizmos.DrawWireCube(cleaverHitbox.position, 
                    cleaverHitbox.GetComponent<BoxCollider2D>().bounds.size);
            }
        }
    }
}
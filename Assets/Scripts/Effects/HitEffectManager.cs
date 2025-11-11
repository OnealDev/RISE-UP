using UnityEngine;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager Instance;

    [Header("Hit Effect Settings")]
    public ParticleSystem hitParticlePrefab;
    public float particleRotationOffset = 0f; // Adjust if your prefab faces the wrong direction


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SpawnHitEffect(Vector2 hitPosition, Vector2 hitDirection)
    {
       
        // Spawns a hit particle effect at the hit position, rotated based on attack direction.

        if (hitParticlePrefab == null)
        {
            Debug.LogWarning("HitEffectManager: No hitParticlePrefab assigned.");
            return;
        }

        // Calculate rotation based on direction
        float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + particleRotationOffset);

        // Spawn particle effect
        ParticleSystem particles = Instantiate(hitParticlePrefab, hitPosition, rotation);

        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
    }
}

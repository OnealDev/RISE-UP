using UnityEngine;

public class InteractiveFlame : MonoBehaviour
{
     // -----------------------------
     // EXTINGUISH SETTINGS
     // -----------------------------
     [Header("Extinguish Settings")]
     public AudioClip extinguishSound;
     public float soundVolume = 1f;

     // NEW — assign smoke/explosion particle in Inspector
     public ParticleSystem extinguishEffect;

     private AudioSource audioSource;
     private bool isExtinguished = false;

     // -----------------------------
     // DAMAGE + KNOCKBACK SETTINGS
     // -----------------------------
     [Header("Damage Settings")]
     public int damage = 1;
     public float damageCooldown = 1f;
     private float lastDamageTime = 0f;

     void Awake()
     {
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
               audioSource = gameObject.AddComponent<AudioSource>();

          audioSource.playOnAwake = false;
     }

     // -----------------------------
     // EXTINGUISH FUNCTION
     // -----------------------------
     public void Extinguish()
     {
          if (isExtinguished) return;
          isExtinguished = true;

          // Play extinguish sound
          if (extinguishSound != null)
               audioSource.PlayOneShot(extinguishSound, soundVolume);

          // NEW — play smoke/explosion particle
          if (extinguishEffect != null)
          {
               extinguishEffect.transform.parent = null;   // detach so it can finish playing
               extinguishEffect.Play();
               Destroy(extinguishEffect.gameObject, 2f);
          }

          // Destroy flame object after sound (if any)
          Destroy(gameObject, extinguishSound != null ? extinguishSound.length : 0f);
     }

     // -----------------------------
     // PROXIMITY DAMAGE + KNOCKBACK
     // -----------------------------
     private void OnTriggerStay2D(Collider2D collision)
     {
          if (isExtinguished) return;

          if (!collision.CompareTag("Player")) return;

          if (Time.time < lastDamageTime + damageCooldown)
               return;

          HealthManager playerHealth = collision.GetComponent<HealthManager>();
          if (playerHealth != null)
          {
               Vector2 knockDirection =
                    (collision.transform.position - transform.position).normalized;

               playerHealth.TakeDamage(damage, knockDirection);
          }

          lastDamageTime = Time.time;
     }
}

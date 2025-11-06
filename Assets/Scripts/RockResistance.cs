using UnityEngine;
using System.Collections;

public class RockResistance : MonoBehaviour
{
     private Rigidbody2D rb;
     private AudioSource audioSource;

     [Header("Rock Push Settings")]
     [Tooltip("Minimum player strength (mass) required to move this rock.")]
     public float requiredStrength = 50f;

     [Tooltip("How much linear damping to apply when player is not strong enough.")]
     public float resistanceDamping = 999f;

     private float normalDamping;
     private bool playerNearby = false;
     private Rigidbody2D playerRb;

     [Header("Shake Settings")]
     [Tooltip("Enable shake effect when the player is too weak to push.")]
     public bool enableShake = true;
     [Tooltip("How much the rock shakes when too heavy.")]
     public float shakeMagnitude = 0.05f;
     [Tooltip("How fast the shake happens.")]
     public float shakeSpeed = 25f;
     [Tooltip("How long the rock shakes each time.")]
     public float shakeDuration = 0.5f;

     private Vector3 originalPosition;
     private bool isShaking = false;

     [Header("Audio Settings")]
     [Tooltip("Rumble sound when player tries to push but is too weak.")]
     public AudioClip rumbleClip;
     [Range(0f, 1f)] public float rumbleVolume = 0.6f;

     void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          normalDamping = rb.linearDamping;
          originalPosition = transform.localPosition;

          // Add an AudioSource if one doesn’t exist
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
          {
               audioSource = gameObject.AddComponent<AudioSource>();
               audioSource.playOnAwake = false;
               audioSource.spatialBlend = 0f; // 2D sound
          }
     }

     void FixedUpdate()
     {
          if (playerNearby && playerRb != null)
          {
               if (playerRb.mass < requiredStrength)
               {
                    rb.linearDamping = resistanceDamping;

                    if (enableShake && !isShaking)
                         StartCoroutine(ShakeRock());
               }
               else
               {
                    rb.linearDamping = normalDamping;
               }
          }
          else
          {
               rb.linearDamping = normalDamping;
          }
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          if (collision.gameObject.CompareTag("Player"))
          {
               playerNearby = true;
               playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
          }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
          if (collision.gameObject.CompareTag("Player"))
          {
               playerNearby = false;
               playerRb = null;
               rb.linearDamping = normalDamping;
               
          }
     }

     private IEnumerator ShakeRock()
     {
          isShaking = true;
          float elapsed = 0f;

          // Play rumble sound once
          if (rumbleClip != null && !audioSource.isPlaying)
               audioSource.PlayOneShot(rumbleClip, rumbleVolume);

          while (elapsed < shakeDuration)
          {
               float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
               transform.localPosition = originalPosition + new Vector3(offsetX, 0f, 0f);
               elapsed += Time.deltaTime;
               yield return null;
          }

          transform.localPosition = originalPosition;
          isShaking = false;
     }
}

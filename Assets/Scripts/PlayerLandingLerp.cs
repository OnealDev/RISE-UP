using UnityEngine;

public class PlayerLandingLerp : MonoBehaviour
{
     public Transform landingPoint;
     public float startHeightOffset = 2f;
     public float lerpDuration = 1.5f;

     [Header("Landing FX")]
     public GameObject dustPrefab;
     public float dustYOffset = -0.2f;

     [Header("Landing Sound")]
     public AudioClip landingSFX;
     public float landingVolume = 1f;
     private AudioSource audioSource;

     [Header("Landing Camera Shake")]
     public float landingShakeDuration = 0.25f;
     public float landingShakeMagnitude = 0.45f;

     private Vector3 startPos;
     private Vector3 endPos;
     private float timer = 0f;
     private bool finished = false;

     private AryasPlayerMovement moveScript;
     private Rigidbody2D rb;

     void Start()
     {
          moveScript = GetComponent<AryasPlayerMovement>();
          rb = GetComponent<Rigidbody2D>();

          audioSource = gameObject.AddComponent<AudioSource>();
          audioSource.playOnAwake = false;

          moveScript.enabled = false;
          rb.simulated = false;

          endPos = landingPoint.position;
          startPos = new Vector3(endPos.x, endPos.y + startHeightOffset, endPos.z);

          transform.position = startPos;
     }

     void Update()
     {
          if (finished) return;

          timer += Time.deltaTime;
          float t = timer / lerpDuration;

          transform.position = Vector3.Lerp(startPos, endPos, t);

          // WHEN LANDING IS COMPLETE
          if (t >= 1f)
          {
               finished = true;

               rb.simulated = true;
               moveScript.enabled = true;

               // --- Dust FX ---
               if (dustPrefab != null)
               {
                    Vector3 dustPos = endPos + new Vector3(0, dustYOffset, 0);
                    Instantiate(dustPrefab, dustPos, Quaternion.identity);
               }

               // --- Camera Shake (LANDING ONLY) ---
               if (ScreenShake.Instance != null)
               {
                    ScreenShake.Instance.Shake(landingShakeDuration, landingShakeMagnitude);
               }

               // --- Landing Sound ---
               if (landingSFX != null)
               {
                    audioSource.PlayOneShot(landingSFX, landingVolume);
               }
          }
     }
}

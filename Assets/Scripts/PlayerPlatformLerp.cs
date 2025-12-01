using UnityEngine;

public class PlayerFloatToPlatform : MonoBehaviour
{
     [Header("Float Path Points")]
     public Transform risePoint;
     public Transform glidePoint;
     public Transform landingPoint;

     [Header("Timings")]
     public float riseDuration = 1f;
     public float glideDuration = 1f;
     public float landDuration = 0.8f;

     private int phase = -1;
     private float timer = 0f;
     private Vector3 startPos;

     private AryasPlayerMovement movementScript;

     [Header("Glow Effect")]
     public SpriteRenderer playerSprite;
     public Color glowColor = new Color(1f, 1f, 0.5f, 1f);
     private Color baseColor;

     [Header("Screen Shake")]
     public float shakeIntensity = 0.25f;
     public float shakeDuration = 0.2f;

     [Header("Audio")]
     public AudioSource audioSource;
     public AudioClip whooshClip;
     public AudioClip landingClip;

     void Start()
     {
          movementScript = GetComponent<AryasPlayerMovement>();
          baseColor = playerSprite.color;
     }

     void Update()
     {
          if (phase < 0) return;

          switch (phase)
          {
               case 0: // RISE
                    timer += Time.deltaTime;
                    float t0 = Mathf.SmoothStep(0, 1, timer / riseDuration);
                    transform.position = Vector3.Lerp(startPos, risePoint.position, t0);

                    // Glow effect
                    playerSprite.color = Color.Lerp(baseColor, glowColor, t0);

                    if (t0 >= 1f)
                    {
                         playerSprite.color = baseColor;
                         phase = 1;
                         timer = 0f;
                         startPos = risePoint.position;
                    }
                    break;

               case 1: // GLIDE
                    timer += Time.deltaTime;
                    float t1 = Mathf.SmoothStep(0, 1, timer / glideDuration);
                    transform.position = Vector3.Lerp(startPos, glidePoint.position, t1);

                    if (t1 >= 1f)
                    {
                         phase = 2;
                         timer = 0f;
                         startPos = glidePoint.position;
                    }
                    break;

               case 2: // LAND
                    timer += Time.deltaTime;
                    float t2 = Mathf.SmoothStep(0, 1, timer / landDuration);
                    transform.position = Vector3.Lerp(startPos, landingPoint.position, t2);

                    if (t2 >= 1f)
                    {
                         phase = 3;

                         // Re-enable movement
                         movementScript.enabled = true;

                         // Play landing sound
                         if (landingClip != null)
                              audioSource.PlayOneShot(landingClip);

                         // Screen shake
                         StartCoroutine(ScreenShake());
                    }
                    break;
          }
     }

     public void BeginFloat()
     {
          if (phase != -1) return;

          movementScript.enabled = false;

          phase = 0;
          timer = 0f;
          startPos = transform.position;

          // Start glow immediately
          playerSprite.color = glowColor;

          // Play whoosh sound
          if (whooshClip != null)
               audioSource.PlayOneShot(whooshClip);
     }

     private System.Collections.IEnumerator ScreenShake()
     {
          Vector3 originalPos = Camera.main.transform.position;
          float elapsed = 0f;

          while (elapsed < shakeDuration)
          {
               float x = Random.Range(-shakeIntensity, shakeIntensity);
               float y = Random.Range(-shakeIntensity, shakeIntensity);

               Camera.main.transform.position = new Vector3(
                   originalPos.x + x,
                   originalPos.y + y,
                   originalPos.z
               );

               elapsed += Time.deltaTime;
               yield return null;
          }

          Camera.main.transform.position = originalPos;
     }
}

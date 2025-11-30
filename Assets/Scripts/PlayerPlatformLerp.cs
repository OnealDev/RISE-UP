using UnityEngine;

public class PlayerFloatToPlatform : MonoBehaviour
{
     [Header("Float Path Points")]
     public Transform risePoint;      // where he floats straight up
     public Transform glidePoint;     // where he glides sideways
     public Transform landingPoint;   // final landing spot

     [Header("Timings")]
     public float riseDuration = 1f;
     public float glideDuration = 1f;
     public float landDuration = 0.8f;

     private int phase = -1; // -1 = not started
     private float timer = 0f;
     private Vector3 startPos;

     private AryasPlayerMovement movementScript;

     void Start()
     {
          movementScript = GetComponent<AryasPlayerMovement>(); // whatever your movement script is called
     }

     void Update()
     {
          if (phase < 0) return;

          switch (phase)
          {
               case 0: // RISE UP
                    timer += Time.deltaTime;
                    float t0 = Mathf.SmoothStep(0, 1, timer / riseDuration);
                    transform.position = Vector3.Lerp(startPos, risePoint.position, t0);

                    if (t0 >= 1f)
                    {
                         phase = 1;
                         timer = 0f;
                         startPos = risePoint.position;
                    }
                    break;

               case 1: // GLIDE SIDEWAYS
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
                         // re-enable movement
                         movementScript.enabled = true;
                    }
                    break;
          }
     }

     public void BeginFloat()
     {
          if (phase != -1) return;

          // Disable movement
          movementScript.enabled = false;

          // Start floating
          phase = 0;
          timer = 0f;
          startPos = transform.position;
     }
}

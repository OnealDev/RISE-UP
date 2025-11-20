using UnityEngine;

public class PlayerLandingLerp : MonoBehaviour
{
     public Transform landingPoint;
     public float startHeightOffset = 2f;
     public float lerpDuration = 1.5f;

     [Header("Landing FX")]
     public GameObject dustPrefab;   // Drag your dust prefab here
     public float dustYOffset = -0.2f; // helps place dust at the feet

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

          if (t >= 1f)
          {
               finished = true;

               // Re-enable movement & physics
               rb.simulated = true;
               moveScript.enabled = true;

               // --- LANDING VISUAL FX ---

               // Dust puff
               if (dustPrefab != null)
               {
                    Vector3 dustPos = endPos + new Vector3(0, dustYOffset, 0);
                    Instantiate(dustPrefab, dustPos, Quaternion.identity);
               }

               // Screen shake (using your script!)
               if (ScreenShake.Instance != null)
               {
                    ScreenShake.Instance.QuickShake();  // or BigShake()
               }
          }
     }
}


using UnityEngine;

public class FlameAnimator : MonoBehaviour
{
     [Header("Visual Settings")]
     public float flickerSpeed = 8f;
     public float minAlpha = 0.6f;
     public float maxAlpha = 1f;

     [Header("Swipe Motion Settings")]
     public float moveDistance = 0.5f;
     public float moveSpeed = 5f;

     [HideInInspector] public bool facingRight = true; // set by DemonAI

     private SpriteRenderer sr;
     private Vector3 originalLocalPos;
     private float flickerTimer;
     private bool movingOut = true;

     void Start()
     {
          sr = GetComponent<SpriteRenderer>();
          originalLocalPos = transform.localPosition;
     }

     void OnEnable()
     {
          transform.localPosition = originalLocalPos;
          flickerTimer = 0;
          movingOut = true;
     }

     void Update()
     {
          // Flicker effect
          flickerTimer += Time.deltaTime * flickerSpeed;
          float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(flickerTimer) + 1) / 2f);
          Color c = sr.color;
          c.a = alpha;
          sr.color = c;

          // Swipe motion
          float step = moveSpeed * Time.deltaTime;
          Vector3 swipeDir = facingRight ? Vector3.right : Vector3.left;

          if (movingOut)
          {
               transform.localPosition = Vector3.MoveTowards(
                   transform.localPosition,
                   originalLocalPos + swipeDir * moveDistance,
                   step
               );
               if (Vector3.Distance(transform.localPosition, originalLocalPos + swipeDir * moveDistance) < 0.01f)
                    movingOut = false;
          }
          else
          {
               transform.localPosition = Vector3.MoveTowards(
                   transform.localPosition,
                   originalLocalPos,
                   step
               );
          }
     }
}

using UnityEngine;
using System.Collections;

public class BounceEffect : MonoBehaviour
{
     [Header("Bounce Settings")]
     public float bounceHeight = 0.5f;
     public float bounceSpeed = 4f;
     public float bounceDuration = 0.5f;

     private Vector3 startPos;
     private bool hasBounced = false;

     void Awake()
     {
          startPos = transform.position;
     }

     // Only runs when explicitly called (like from the chest)
     public void StartBounce()
     {
          if (!hasBounced)
          {
               hasBounced = true;
               StartCoroutine(Bounce());
          }
     }

     private IEnumerator Bounce()
     {
          float elapsed = 0f;

          while (elapsed < bounceDuration)
          {
               float yOffset = Mathf.Sin(elapsed * bounceSpeed) * bounceHeight;
               transform.position = startPos + new Vector3(0f, yOffset, 0f);

               elapsed += Time.deltaTime;
               yield return null;
          }

          transform.position = startPos;
     }
}

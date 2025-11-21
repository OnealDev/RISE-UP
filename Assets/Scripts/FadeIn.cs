using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
     public CanvasGroup fadePanel;
     public float fadeDuration = 1.2f;

     void Start()
     {
          StartCoroutine(FadeInRoutine());
     }

     IEnumerator FadeInRoutine()
     {
          float t = 0f;

          while (t < fadeDuration)
          {
               t += Time.deltaTime;
               fadePanel.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
               yield return null;
          }

          fadePanel.alpha = 0f;
     }
}

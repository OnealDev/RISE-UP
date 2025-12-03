using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager2 : MonoBehaviour
{
     public static FadeManager2 Instance;
     public CanvasGroup fadePanel;
     public float fadeSpeed = 1.2f;

     private void Awake()
     {
          if (Instance == null)
          {
               Instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else
          {
               Destroy(gameObject);
          }
     }

     private void Start()
     {
          // Fade in automatically when a scene loads
          fadePanel.alpha = 1;
          FadeIn();
     }

     public void FadeIn()
     {
          StartCoroutine(Fade(1, 0));
     }

     public void FadeOutAndLoad(string sceneName)
     {
          StartCoroutine(FadeOutLoad(sceneName));
     }

     private IEnumerator Fade(float from, float to)
     {
          float t = 0f;

          while (t < 1)
          {
               t += Time.deltaTime * fadeSpeed;
               fadePanel.alpha = Mathf.Lerp(from, to, t);
               yield return null;
          }
     }

     private IEnumerator FadeOutLoad(string sceneName)
     {
          // Fade to black
          yield return StartCoroutine(Fade(0, 1));

          // Load scene
          SceneManager.LoadScene(sceneName);

          // Fade in again
          yield return null;
          FadeIn();
     }
}

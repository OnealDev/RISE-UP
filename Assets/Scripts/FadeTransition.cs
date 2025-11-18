using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
     public static FadeTransition instance;
     public Image fadePanelTwo;
     public float fadeSpeed = 1f;

     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else
          {
               Destroy(gameObject);
          }
     }

     public void FadeToScene(string sceneName)
     {
          StartCoroutine(FadeAndSwitch(sceneName));
     }

     private IEnumerator FadeAndSwitch(string sceneName)
     {
          // Fade to black
          float alpha = 0;
          while (alpha < 1)
          {
               alpha += Time.deltaTime * fadeSpeed;
               fadePanelTwo.color = new Color(0, 0, 0, alpha);
               yield return null;
          }

          // Load scene
          SceneManager.LoadScene(sceneName);

          // Fade from black
          while (alpha > 0)
          {
               alpha -= Time.deltaTime * fadeSpeed;
               fadePanelTwo.color = new Color(0, 0, 0, alpha);
               yield return null;
          }
     }
}

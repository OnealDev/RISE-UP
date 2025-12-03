using UnityEngine;

public class SceneTransition : MonoBehaviour
{
     [Header("Name of the scene to load")]
     public string sceneName = "Final Boss";

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               FadeManager2.Instance.FadeOutAndLoad(sceneName);
          }
     }
}

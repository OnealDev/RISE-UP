using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
     [Header("Name of the scene to load")]
     public string sceneName = "Final Boss"; // <-- set this to EXACT scene name

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               SceneManager.LoadScene(sceneName);
          }
     }
}

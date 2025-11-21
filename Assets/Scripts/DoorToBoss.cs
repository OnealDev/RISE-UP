using UnityEngine;

public class DoorToBoss : MonoBehaviour
{
     public string sceneToLoad = "Final Boss";

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player"))
          {
               FadeTransition.instance.FadeToScene(sceneToLoad);
          }
     }
}

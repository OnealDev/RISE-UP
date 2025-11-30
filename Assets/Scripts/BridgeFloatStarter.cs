using UnityEngine;

public class BridgeFloatStarter : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other)
     {
          if (other.CompareTag("Player"))
          {
               other.GetComponent<PlayerFloatToPlatform>()?.BeginFloat();
          }
     }
}

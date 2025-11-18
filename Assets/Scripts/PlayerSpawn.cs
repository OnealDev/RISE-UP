using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
     void Start()
     {
          GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");

          if (spawnPoint != null)
          {
               transform.position = spawnPoint.transform.position;
          }
          else
          {
               Debug.LogWarning(" No PlayerSpawn object found in this scene!");
          }
     }
}

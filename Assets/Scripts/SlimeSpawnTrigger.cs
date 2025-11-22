using UnityEngine;

public class SlimeSpawnTrigger : MonoBehaviour
{
     public GameObject slimePrefab;
     public Transform[] spawnPoints;
     private bool hasSpawned = false;

     private void OnTriggerEnter2D(Collider2D collision)
     {
          if (collision.CompareTag("Player") && !hasSpawned)
          {
               hasSpawned = true;

               foreach (Transform point in spawnPoints)
               {
                    GameObject slime = Instantiate(slimePrefab, point.position, Quaternion.identity);

                    // Assign follow target so they actually chase the player
                    EnemyFollow follow = slime.GetComponent<EnemyFollow>();
                    if (follow != null)
                    {
                         follow.followTarget = collision.transform; // the player
                    }
               }
          }
     }
}

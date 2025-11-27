using UnityEngine;
using System.Collections;

public class FireRingSpawner : MonoBehaviour
{
     [Header("Setup")]
     public GameObject flamePrefab;   // your fire prefab
     public int numberOfFlames = 12;  // how many around the ring
     public float radius = 2f;

     [Header("Spawn Timing")]
     public float spawnDelay = 0.2f;  // time between each flame

     private bool hasSpawned = false;

     private void OnTriggerEnter2D(Collider2D other)
     {
          if (hasSpawned) return;

          if (other.CompareTag("Player"))
          {
               hasSpawned = true;
               StartCoroutine(SpawnFireRing());
          }
     }

     IEnumerator SpawnFireRing()
     {
          for (int i = 0; i < numberOfFlames; i++)
          {
               float angle = i * Mathf.PI * 2f / numberOfFlames;

               Vector3 pos = new Vector3(
                   Mathf.Cos(angle) * radius,
                   Mathf.Sin(angle) * radius,
                   0f
               );

               Instantiate(flamePrefab, transform.position + pos, Quaternion.identity);

               yield return new WaitForSeconds(spawnDelay);
          }
     }
}


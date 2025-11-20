using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
     public Transform[] waypoints;
     public float speed = 2f;
     private int currentIndex = 0;

     void Update()
     {
          if (waypoints.Length == 0) return;

          // Move toward next waypoint
          transform.position = Vector2.MoveTowards(
              transform.position,
              waypoints[currentIndex].position,
              speed * Time.deltaTime
          );

          // Check if reached waypoint
          if (Vector2.Distance(transform.position, waypoints[currentIndex].position) < 0.1f)
          {
               currentIndex++;

               // Loop back to start if at end
               if (currentIndex >= waypoints.Length)
                    currentIndex = 0;
          }
     }
}


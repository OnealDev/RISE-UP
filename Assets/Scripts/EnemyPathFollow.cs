using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
     public Transform[] waypoints;
     public float speed = 2f;
     private int currentIndex = 0;

     [HideInInspector]
     public bool isPaused = false;

     void Update()
     {
          if (isPaused || waypoints.Length == 0)
               return;

          // Move to next waypoint
          transform.position = Vector2.MoveTowards(
              transform.position,
              waypoints[currentIndex].position,
              speed * Time.deltaTime
          );

          // Switch waypoint if reached
          if (Vector2.Distance(transform.position, waypoints[currentIndex].position) < 0.1f)
          {
               currentIndex++;

               if (currentIndex >= waypoints.Length)
                    currentIndex = 0;
          }
     }
}



using UnityEngine;

public class EnemyKillManager : MonoBehaviour
{
     public static EnemyKillManager Instance;

     public int killsNeeded = 8;
     public int currentKills = 0;

     void Awake()
     {
          if (Instance == null)
               Instance = this;
          else
               Destroy(gameObject);
     }

     public void RegisterKill()
     {
          currentKills++;

          Debug.Log("Kills: " + currentKills);

          if (currentKills >= killsNeeded)
          {
               Debug.Log("Rock can now be moved!");
          }
     }

     public bool CanMoveRock()
     {
          return currentKills >= killsNeeded;
     }
}

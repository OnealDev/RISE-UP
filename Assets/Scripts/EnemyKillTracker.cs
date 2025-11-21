using UnityEngine;

public class EnemyKillTracker : MonoBehaviour
{
     public static EnemyKillTracker Instance;

     [Header("Slime Kill Requirement")]
     public int requiredKills = 5;   // Set how many slimes needed
     public int currentKills = 0;

     void Awake()
     {
          Instance = this;
     }

     public void AddKill()
     {
          currentKills++;
          Debug.Log("Slime killed! Total: " + currentKills);
     }

     public bool HasMetRequirement()
     {
          return currentKills >= requiredKills;
     }
}

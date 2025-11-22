
using UnityEngine;
using TMPro; // Only if using TextMeshPro

public class KillCounterUI : MonoBehaviour
{
     public TMP_Text killText; // Assign in Inspector

     void Update()
     {
          if (EnemyKillTracker.Instance != null)
          {
               killText.text = "LavaSlime Kills: " + EnemyKillTracker.Instance.currentKills;
          }
     }
}

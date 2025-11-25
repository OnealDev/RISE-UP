using UnityEngine;

public class FinalBossStartPoint : MonoBehaviour
{
     void Start()
     {
          GameObject player = GameObject.FindGameObjectWithTag("Player");

          if (player != null)
               player.transform.position = transform.position;
     }
}

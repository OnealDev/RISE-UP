using UnityEngine;

public class BossRoarOnSpawn : MonoBehaviour
{
     public AudioSource roarSource;
     public float delayBeforeRoar = 0.5f;

     void Start()
     {
          if (roarSource != null)
               Invoke(nameof(TriggerRoar), delayBeforeRoar);
     }

     void TriggerRoar()
     {
          // Play sound
          roarSource.Play();

          // Shake camera (your existing system)
          if (ScreenShake.Instance != null)
               ScreenShake.Instance.BigShake();
     }
}

using UnityEngine;

public class BreakableRock : MonoBehaviour
{
     [Header("Rock Settings")]
     public int maxHealth = 3;
     private int currentHealth;

     [Header("Effects")]
     public GameObject breakEffectPrefab;
     public AudioClip breakSound;
     private AudioSource audioSource;

     private void Start()
     {
          currentHealth = maxHealth;
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
               audioSource = gameObject.AddComponent<AudioSource>();
     }

     public void TakeDamage(int damage)
     {
          currentHealth -= damage;

          if (currentHealth <= 0)
          {
               Break();
          }
     }

     private void Break()
     {
          if (breakEffectPrefab)
               Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);

          if (breakSound)
               audioSource.PlayOneShot(breakSound);

          Destroy(gameObject);
     }
}

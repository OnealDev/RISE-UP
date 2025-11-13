using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DemonHitSound : MonoBehaviour
{
     [Header("Attack Sound Settings")]
     [Tooltip("Sound played when the demon attacks.")]
     public AudioClip attackSound;

     [Header("Hurt Sound Settings")]
     [Tooltip("Sound played when the demon takes damage.")]
     public AudioClip hurtSound;

     [Header("Death Sound Settings")]
     [Tooltip("Sound played when the demon dies.")]
     public AudioClip deathSound;

     [Header("Audio Variation Settings")]
     [Tooltip("Pitch range for slight variation each time a sound plays.")]
     public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
     [Tooltip("Volume range for slight variation each time a sound plays.")]
     public Vector2 volumeRange = new Vector2(0.85f, 1.0f);

     private AudioSource audioSource;

     private void Awake()
     {
          // Make sure the demon always has an AudioSource
          audioSource = GetComponent<AudioSource>();
          if (audioSource == null)
               audioSource = gameObject.AddComponent<AudioSource>();

          // Configure audio
          audioSource.playOnAwake = false;
          audioSource.spatialBlend = 0f; // 2D sound for 2D games
     }

     /// <summary>
     /// Plays the attack sound with random pitch/volume.
     /// </summary>
     public void PlayAttackSound()
     {
          PlaySound(attackSound);
     }

     /// <summary>
     /// Plays the hurt sound with random pitch/volume.
     /// </summary>
     public void PlayHurtSound()
     {
          PlaySound(hurtSound);
     }

     /// <summary>
     /// Plays the death sound with random pitch/volume.
     /// </summary>
     public void PlayDeathSound()
     {
          PlaySound(deathSound);
     }

     // Core playback logic for all sound types
     private void PlaySound(AudioClip clip)
     {
          if (clip == null || audioSource == null)
               return;

          audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
          audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
          audioSource.PlayOneShot(clip);
     }
}

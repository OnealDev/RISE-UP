using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioClip hitSound;
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);//This will be for slight pitch variation on each attack
    public Vector2 volumeRange = new Vector2(0.85f, 1.0f); //Volume variation on each attack
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayRandomHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.PlayOneShot(hitSound);
        }
    }
}

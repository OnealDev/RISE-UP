using UnityEngine;
using System.Collections;

public class FlashOnHit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    // ⭐ ADD THIS ⭐
    public IEnumerator StartInvincibilityFlash(float duration)
    {
        float flashSpeed = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashSpeed);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashSpeed);

            timer += flashSpeed * 2f;
        }

        spriteRenderer.enabled = true;
    }
}

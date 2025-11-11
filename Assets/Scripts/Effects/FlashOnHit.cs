using System.Collections;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        //Flash white 
        spriteRenderer.color = Color.red;

        //wait short period of time
        yield return new WaitForSeconds(0.3f);

        //return color
        spriteRenderer.color = originalColor;
    }
}

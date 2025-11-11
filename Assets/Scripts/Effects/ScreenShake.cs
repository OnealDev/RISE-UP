using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    [Header("Screen Shake Settings")]
    public float defaultDuration = 0.2f;
    public float defaultMagnitude = 0.1f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Shake(float duration = 0.2f, float magnitude = 0.1f)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0;

        while (elapsed < duration)
        {
            //Random Shake
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;


            //Apply
            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        //Reset position
        transform.localPosition = originalPos;
    }

    //Quick shakes
    public void QuickShake()
    {
        Shake(0.15f, 0.08f);
    }
    
    //Big Shakes
    public void BigShake()
    {
        Shake(0.3f, 0.2f);
    }
}

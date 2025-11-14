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
            Debug.Log("✅ ScreenShake Instance created!");
        }
        else
        {
            Debug.Log("❌ ScreenShake Instance already exists - destroying duplicate");
            Destroy(gameObject);
        }
    }
    
    public void Shake(float duration = 0.2f, float magnitude = 0.1f)
    {
        Debug.Log($"🎬 ScreenShake called! Duration: {duration}, Magnitude: {magnitude}");
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Debug.Log("🔄 Starting shake routine...");
        Vector3 originalPos = transform.position;
        float elapsed = 0;

        while (elapsed < duration)
        {
            //Random Shake
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;

            //Apply
            transform.position = originalPos + new Vector3(x, y, 0);
            Debug.Log($"📱 Camera position: {transform.position}");

            elapsed += Time.deltaTime;
            yield return null;
        }

        //Reset position
        transform.position = originalPos;
        Debug.Log("✅ Shake routine finished - camera reset");
    }

    //Quick shakes
    public void QuickShake()
    {
        Debug.Log("🎬 QuickShake called!");
        Shake(0.15f, 0.08f);
    }
    
    //Big Shakes
    public void BigShake()
    {
        Debug.Log("🎬 BigShake called!");
        Shake(0.3f, 0.2f);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Press T to test shake
        {
            Debug.Log("🧪 Manual shake test triggered by T key");
            BigShake();
        }
    }
}
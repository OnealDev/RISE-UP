using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    public int totalSegments = 6;

    public void SetHealth(int currentHealth)
    {
        if (fillImage != null)
        {
            float healthPercent = (float)currentHealth / totalSegments;
            fillImage.fillAmount = healthPercent;
        }
    }
}
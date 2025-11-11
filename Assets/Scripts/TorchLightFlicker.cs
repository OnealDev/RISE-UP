using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLightFlicker : MonoBehaviour
{
     private Light2D torchLight;
     public float minIntensity = 1.2f;
     public float maxIntensity = 1.6f;
     public float flickerSpeed = 0.1f;

     void Awake()
     {
          torchLight = GetComponent<Light2D>();
     }

     void Update()
     {
          if (torchLight != null)
          {
               torchLight.intensity = Mathf.Lerp(torchLight.intensity,
                   Random.Range(minIntensity, maxIntensity),
                   flickerSpeed);
          }
     }
}

using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
     public float speed = 4f;
     private TextMeshProUGUI text;

     void Start()
     {
          text = GetComponent<TextMeshProUGUI>();
     }

     void Update()
     {
          float alpha = (Mathf.Sin(Time.time * speed) + 1) * 0.5f;
          Color c = text.color;
          c.a = alpha;
          text.color = c;
     }
}

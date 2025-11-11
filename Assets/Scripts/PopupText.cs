using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
     [Header("Popup Settings")]
     [TextArea]
     public string message;          // Write your custom phrase here in the Inspector
     public float moveSpeed = 1f;
     public float fadeDuration = 2f;
     public Color textColor = Color.white;

     private TextMeshPro textMesh;
     private Color originalColor;

     void Awake()
     {
          textMesh = GetComponent<TextMeshPro>();

          if (textMesh != null)
          {
               // Set text and color from Inspector values
               textMesh.text = message;
               textMesh.color = textColor;
               originalColor = textMesh.color;
          }
     }

     void Update()
     {
          if (textMesh == null) return;

          // Float upward
          transform.position += Vector3.up * moveSpeed * Time.deltaTime;

          // Fade out
          Color color = textMesh.color;
          color.a -= Time.deltaTime / fadeDuration;
          textMesh.color = color;

          // Destroy when invisible
          if (textMesh.color.a <= 0)
               Destroy(gameObject);
     }

     // Optional: allow scripts to change text dynamically if you ever want
     public void SetText(string newText, Color? newColor = null)
     {
          if (textMesh == null) return;

          textMesh.text = newText;
          textMesh.color = newColor ?? textMesh.color;
     }
}


using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
     public float moveSpeed = 1f;
     public float fadeDuration = 2f;

     private TextMeshPro textMesh;
     private Color originalColor;

     void Awake()
     {
          textMesh = GetComponent<TextMeshPro>();
          originalColor = textMesh.color;
     }

     void Update()
     {
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

     public void SetText(string message)
     {
          textMesh.text = message;
          textMesh.color = originalColor;
     }
}

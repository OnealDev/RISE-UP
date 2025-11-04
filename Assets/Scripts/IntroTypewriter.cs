using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTypewriter : MonoBehaviour
{
     [TextArea] public string fullText; // Type your text in Inspector
     public float typingSpeed = 0.05f;
     public float displayTime = 3f;
     public float fadeOutTime = 1.5f;

     private TextMeshProUGUI textMesh;

     void Start()
     {
          textMesh = GetComponent<TextMeshProUGUI>();
          textMesh.text = "";
          StartCoroutine(TypeText());
     }

     IEnumerator TypeText()
     {
          // Typewriter effect
          foreach (char c in fullText)
          {
               textMesh.text += c;
               yield return new WaitForSeconds(typingSpeed);
          }

          // Wait before fading out
          yield return new WaitForSeconds(displayTime);

          // Fade out the text
          Color color = textMesh.color;
          float elapsed = 0f;
          while (elapsed < fadeOutTime)
          {
               elapsed += Time.deltaTime;
               color.a = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
               textMesh.color = color;
               yield return null;
          }

          // Disable the entire UI (banner + text)
          transform.root.gameObject.SetActive(false);
     }
}

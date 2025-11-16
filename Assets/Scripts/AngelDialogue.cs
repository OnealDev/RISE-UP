using UnityEngine;
using TMPro;
using System.Collections;

public class AngelDialogue : MonoBehaviour
{
     [Header("UI References")]
     public GameObject dialoguePanel;
     public TextMeshProUGUI dialogueText;

     [Header("Typewriter Settings")]
     public float typingSpeed = 0.05f;
     public float displayDuration = 2f;
     public float fadeOutTime = 1.5f;

     private Coroutine currentRoutine;

     void Awake()
     {
          if (dialoguePanel != null)
               dialoguePanel.SetActive(false);

          if (dialogueText != null)
               dialogueText.text = "";
     }

     public void ShowDialogue(string message)
     {
          if (currentRoutine != null)
               StopCoroutine(currentRoutine);

          dialoguePanel.SetActive(true);
          dialogueText.color = new Color(1, 1, 1, 1); // reset alpha
          currentRoutine = StartCoroutine(TypeAndFade(message));
     }

     IEnumerator TypeAndFade(string message)
     {
          dialogueText.text = "";

          foreach (char c in message)
          {
               dialogueText.text += c;
               yield return new WaitForSeconds(typingSpeed);
          }

          yield return new WaitForSeconds(displayDuration);

          // Fade out
          float elapsed = 0f;
          Color color = dialogueText.color;
          while (elapsed < fadeOutTime)
          {
               elapsed += Time.deltaTime;
               color.a = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
               dialogueText.color = color;
               yield return null;
          }

          dialoguePanel.SetActive(false);
     }
}


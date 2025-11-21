using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AngelDialogue : MonoBehaviour
{
     [Header("UI References")]
     public GameObject dialoguePanel;
     public TextMeshProUGUI dialogueText;
     public Button nextButton;

     [Header("Typewriter Settings")]
     public float typingSpeed = 0.05f;

     private List<string> dialogueLines;
     private int currentIndex = 0;
     private Coroutine typingRoutine;
     private bool isTyping = false;

     void Awake()
     {
          dialoguePanel.SetActive(false);
          dialogueText.text = "";
          nextButton.gameObject.SetActive(false);
          nextButton.onClick.AddListener(ShowNextLine);
     }

     public void ShowDialogue(List<string> lines)
     {
          dialogueLines = lines;
          currentIndex = 0;
          dialoguePanel.SetActive(true);
          nextButton.gameObject.SetActive(false);
          ShowCurrentLine();
     }

     void ShowCurrentLine()
     {
          if (typingRoutine != null) StopCoroutine(typingRoutine);
          typingRoutine = StartCoroutine(TypeLine(dialogueLines[currentIndex]));
     }

     IEnumerator TypeLine(string line)
     {
          isTyping = true;
          dialogueText.text = "";
          foreach (char c in line)
          {
               dialogueText.text += c;
               yield return new WaitForSeconds(typingSpeed);
          }
          isTyping = false;

          // FIX #2 — Next button ALWAYS shows, even on the last line
          nextButton.gameObject.SetActive(true);
     }

     void ShowNextLine()
     {
          if (isTyping)
          {
               // Skip typing and show full line instantly
               StopCoroutine(typingRoutine);
               dialogueText.text = dialogueLines[currentIndex];
               isTyping = false;

               // Same change here — button stays active
               nextButton.gameObject.SetActive(true);
               return;
          }

          currentIndex++;
          if (currentIndex < dialogueLines.Count)
          {
               nextButton.gameObject.SetActive(false);
               ShowCurrentLine();
          }
          else
          {
               EndDialogue();
          }
     }

     void EndDialogue()
     {
          dialogueText.text = "";     // Clears leftover text
          dialoguePanel.SetActive(false);
          nextButton.gameObject.SetActive(false);
     }
}

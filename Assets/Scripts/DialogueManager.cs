using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
     public static DialogueManager Instance;

     public GameObject dialoguePanel;
     public TMP_Text dialogueText;

     private bool isShowing = false;

     private void Awake()
     {
          if (Instance == null)
               Instance = this;
          else
               Destroy(gameObject);

          dialoguePanel.SetActive(false);
     }

     public void ShowDialogue(string text)
     {
          dialogueText.text = text;
          dialoguePanel.SetActive(true);
          isShowing = true;
     }

     public void HideDialogue()
     {
          dialoguePanel.SetActive(false);
          isShowing = false;
     }

     private void Update()
     {
          if (isShowing && UnityEngine.Input.GetKeyDown(KeyCode.E))
          {
               HideDialogue();
          }
     }
}

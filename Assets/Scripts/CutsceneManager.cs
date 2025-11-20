using UnityEngine;
using UnityEngine.SceneManagement;


public class CutsceneManager : MonoBehaviour
{
     public GameObject[] cutsceneImages;
     public GameObject continuePrompt;    // OPTIONAL: a “Click to Continue” UI object
     public float displayTime = 5f;

     private int currentIndex = 0;
     private float timer;

     void Start()
     {
          ShowCutscene(0);
          timer = displayTime;

          if (continuePrompt != null)
               continuePrompt.SetActive(false);
     }

     void Update()
     {
          timer -= Time.deltaTime;

          // --- PLAYER INPUT TO ADVANCE ---
          bool advanceInput =
              Input.GetMouseButtonDown(0) ||     // Left-click
              Input.GetKeyDown(KeyCode.Space) || // Spacebar
              Input.GetKeyDown(KeyCode.Return);  // Enter key

          if (advanceInput)
          {
               NextCutscene();
               return;
          }

          // --- TIMER AUTO-ADVANCE ---
          if (timer <= 0f)
          {
               NextCutscene();
          }
     }

     void ShowCutscene(int index)
     {
          // Turn one image on, others off
          for (int i = 0; i < cutsceneImages.Length; i++)
               cutsceneImages[i].SetActive(i == index);

          timer = displayTime;

          if (continuePrompt != null)
               continuePrompt.SetActive(true);
     }

     void NextCutscene()
     {
          if (continuePrompt != null)
               continuePrompt.SetActive(false);

          currentIndex++;

          if (currentIndex < cutsceneImages.Length)
          {
               ShowCutscene(currentIndex);
          }
          else
          {
               Debug.Log("Cutscene finished! Loading Level 1...");
               SceneManager.LoadScene("Level 1");
          }
     }
}

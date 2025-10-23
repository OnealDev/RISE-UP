using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
     public GameObject[] cutsceneImages; // Drag & drop your cutscene images into this array in the Inspector
     private int currentIndex = 0;
     private float timer;
     public float displayTime = 5f; // Each cutscene stays for 5 seconds

     void Start()
     {
          ShowCutscene(0);
          timer = displayTime;
     }

     void Update()
     {
          timer -= Time.deltaTime;

          if (timer <= 0f)
          {
               NextCutscene();
          }
     }

     void ShowCutscene(int index)
     {
          for (int i = 0; i < cutsceneImages.Length; i++)
          {
               cutsceneImages[i].SetActive(i == index);
          }
          timer = displayTime; // Reset the timer for each cutscene
     }

     void NextCutscene()
     {
          currentIndex++;
          if (currentIndex < cutsceneImages.Length)
          {
               ShowCutscene(currentIndex);
          }
          else
          {
               Debug.Log("Cutscene finished! Loading Level 1...");
               SceneManager.LoadScene("Level 1"); // Loads your Level 1 scene
          }
     }
}

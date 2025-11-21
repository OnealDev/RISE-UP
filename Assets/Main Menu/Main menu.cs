using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void NewGame()
     {
          // Start a fresh game (cutscene or first level)
          SceneManager.LoadScene("CutScene");

          // Clear load flag
          PlayerPrefs.SetInt("LoadFromSave", 0);
     }

     public void LoadGame()
     {
          SaveData data = SaveSystem.LoadFile();

          if (data == null)
          {
               Debug.Log("No save file found!");
               return;
          }

          // Store saved position temporarily
          PlayerPrefs.SetFloat("SavedX", data.playerPosition.x);
          PlayerPrefs.SetFloat("SavedY", data.playerPosition.y);
          PlayerPrefs.SetFloat("SavedZ", data.playerPosition.z);

          // Tell the game scene to load player from save
          PlayerPrefs.SetInt("LoadFromSave", 1);

          // Load the saved map (ex: Level1, Cave, Sanctuary, etc.)
          SceneManager.LoadScene(data.currentMap);
     }

     public void ExitGame()
     {
          Application.Quit();
          Debug.Log("Quit Game pressed"); // Works only in Unity Editor
     }
}

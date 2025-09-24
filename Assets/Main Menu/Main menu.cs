using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public void NewGame()
     {
          // Go to cutscenes first
          SceneManager.LoadScene("CutScene");
     }

     public void ExitGame()
     {
          Application.Quit();
          Debug.Log("Quit Game pressed"); // Works only in editor
     }
}

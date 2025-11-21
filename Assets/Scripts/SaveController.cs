using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
     public Vector3 playerPosition;
     public string currentMap;
}

public static class SaveSystem
{
     private static string saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

     public static bool SaveExists()
     {
          return File.Exists(saveLocation);
     }

     public static SaveData LoadFile()
     {
          if (!SaveExists())
               return null;

          return JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
     }

     public static void WriteFile(SaveData data)
     {
          File.WriteAllText(saveLocation, JsonUtility.ToJson(data, true));
     }
}

public class SaveController : MonoBehaviour
{
     private string saveLocation;
     public GameObject player;

     void Start()
     {
          saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

          if (player == null)
               player = GameObject.FindGameObjectWithTag("Player");

          // Did we come from main menu -> Load Game?
          if (PlayerPrefs.GetInt("LoadFromSave", 0) == 1)
          {
               LoadGame();
               PlayerPrefs.SetInt("LoadFromSave", 0);
          }
     }

     public void SaveGame()
     {
          if (player == null) return;

          SaveData saveData = new SaveData
          {
               playerPosition = player.transform.position,
               currentMap = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
          };

          SaveSystem.WriteFile(saveData);
          Debug.Log("Game Saved: " + saveLocation);
     }

     public void LoadGame()
     {
          SaveData saveData = SaveSystem.LoadFile();

          if (saveData == null)
          {
               Debug.Log("No save file found!");
               return;
          }

          if (player != null)
          {
               player.transform.position = saveData.playerPosition;
               Debug.Log("Game Loaded: Player moved to saved position.");
          }
     }
}

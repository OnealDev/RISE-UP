using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
     public Vector3 playerPosition;
     public string currentMap; // optional, if you want to track which map the player was on
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

          LoadGame();
     }

     public void SaveGame()
     {
          if (player == null) return;

          SaveData saveData = new SaveData
          {
               playerPosition = player.transform.position,
               currentMap = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
          };

          File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData, true));
          Debug.Log("Game Saved to " + saveLocation);
     }

     public void LoadGame()
     {
          if (File.Exists(saveLocation))
          {
               SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

               if (player != null)
               {
                    player.transform.position = saveData.playerPosition;
                    Debug.Log("Game Loaded: Player moved to saved position.");
               }
          }
          else
          {
               Debug.Log("No save file found. Creating new save...");
               SaveGame();
          }
     }
}

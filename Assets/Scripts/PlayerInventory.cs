using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
     public int keyCount = 0;

     public void AddKey()
     {
          keyCount++;
          Debug.Log("Keys collected: " + keyCount);
     }
}

using UnityEngine;

public class InventoryController : MonoBehaviour
{
     public GameObject InventoryPanel;  // The parent UI panel holding all slots
     public GameObject SlotPrefab;      // The slot prefab
     public GameObject[] ItemPrefabs;   // Array of item prefabs (Bible, etc.)

     // Add a new item to the inventory
     public void AddItem(int itemID)
     {
          // Make sure we have valid data
          if (ItemPrefabs == null || ItemPrefabs.Length == 0)
          {
               Debug.LogWarning("ItemPrefabs not set in InventoryController!");
               return;
          }

          // Prevent out-of-range errors
          if (itemID < 0 || itemID >= ItemPrefabs.Length)
          {
               Debug.LogWarning($"Invalid item ID: {itemID}");
               return;
          }

          // Go through all slots under the Inventory Panel
          foreach (Transform slotTransform in InventoryPanel.transform)
          {
               Slot slot = slotTransform.GetComponent<Slot>();

               // If the slot exists and is empty
               if (slot != null && slot.currentItem == null)
               {
                    // Instantiate the item prefab inside this slot
                    GameObject newItem = Instantiate(ItemPrefabs[itemID], slotTransform);

                    // Reset local position and scale so it fits the slot
                    RectTransform rect = newItem.GetComponent<RectTransform>();
                    if (rect != null)
                    {
                         rect.anchoredPosition = Vector2.zero;
                         rect.localScale = Vector3.one;
                    }

                    // Assign to the slot
                    slot.currentItem = newItem;

                    Debug.Log($"Added {newItem.name} (ID: {itemID}) to slot {slotTransform.name}");
                    return;
               }
          }

          Debug.Log("Inventory is full! Could not add item.");
     }

     // Remove an item from the inventory
     public void RemoveItem(int itemID, bool dropInWorld = false, Transform dropPoint = null)
     {
          foreach (Transform slotTransform in InventoryPanel.transform)
          {
               Slot slot = slotTransform.GetComponent<Slot>();
               if (slot == null || slot.currentItem == null) continue;

               Item itemComponent = slot.currentItem.GetComponent<Item>();
               if (itemComponent != null && itemComponent.ID == itemID)
               {
                    GameObject itemToRemove = slot.currentItem;
                    slot.currentItem = null; // Clear the slot

                    // Optionally drop into world
                    if (dropInWorld && dropPoint != null)
                    {
                         GameObject droppedItem = Instantiate(itemToRemove, dropPoint.position, Quaternion.identity);
                         if (droppedItem.TryGetComponent<Collider2D>(out var col))
                              col.enabled = true;

                         Debug.Log($"Dropped {itemComponent.Name} into the world.");
                    }

                    Destroy(itemToRemove);
                    Debug.Log($"Removed {itemComponent.Name} (ID: {itemID}) from inventory.");
                    return;
               }
          }

          Debug.LogWarning($"Item with ID {itemID} not found in inventory.");
     }
}


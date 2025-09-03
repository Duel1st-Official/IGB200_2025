using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int quantity;
    public GameObject itemPrefab;

}

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string itemName, int amount, GameObject prefab = null)
    {
        InventoryItem existingItem = items.Find(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity += amount;
        }
        else
        {
            items.Add(new InventoryItem
            {
                itemName = itemName,
                quantity = amount,
                itemPrefab = prefab
            });
        }
    }

    public bool RemoveItem(string itemName, int amount)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);

        if (item != null && item.quantity >= amount)
        {
            item.quantity -= amount;

            //remove if hit zero
            if (item.quantity <= 0)
            {
                items.Remove(item);
            }

            return true;
        }

        return false;
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.itemName == itemName && item.quantity > 0);
    }

    public int GetItemCount(string itemName)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);
        return item != null ? item.quantity : 0;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    public ItemSO[] allItems;
    private Dictionary<string, ItemSO> itemDictionary = new Dictionary<string, ItemSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        BuildItemDictionary();
    }

    private void BuildItemDictionary()
    {
        itemDictionary.Clear();
        foreach (var item in allItems)
        {
            if (!itemDictionary.ContainsKey(item.itemID))
            {
                itemDictionary.Add(item.itemID, item);
            }
            else
            {
                Debug.LogError($"Duplicate item ID found: {item.itemID}");
            }
        }
    }

    public ItemSO GetItem(string itemID)
    {
        if (itemDictionary.TryGetValue(itemID, out ItemSO item))
        {
            return item;
        }
        Debug.LogError($"Item not found with ID: {itemID}");
        return null;
    }
}

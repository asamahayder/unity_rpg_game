using System;
using System.Collections.Generic;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].itemID = i;
            GetItem.Add(i, items[i]);
        }
    }

    
}
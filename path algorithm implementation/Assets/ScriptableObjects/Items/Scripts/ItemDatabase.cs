using System;
using System.Collections.Generic;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<ulong, ItemObject> GetItem = new Dictionary<ulong, ItemObject>();
    
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<ulong, ItemObject>();
    }

    public void OnAfterDeserialize()
    {
        for (ulong i = 0; i < Convert.ToUInt64(items.Length); i++)
        {
            items[i].itemID = i;
            GetItem.Add(i, items[i]);
        }
    }

    
}
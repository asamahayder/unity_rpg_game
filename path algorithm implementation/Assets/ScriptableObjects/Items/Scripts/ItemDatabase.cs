using System;
using System.Collections.Generic;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

// Item Database. Contains all the items in the game
[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] itemObjects;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    
    // When starting up Unity, create this empty list of itemObjects
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }

    // When running the game, instantiate all the items in the game and update their IDs to match the database ids
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            itemObjects[i].itemID = i;
            GetItem.Add(i, itemObjects[i]);
        }
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum itemType {
        Equipment, 
        Consumable, 
        Quest, 
        Currency, 
        Resource, 
        Miscellaneous
    }
    public abstract class ItemObject : ScriptableObject
    {
        public ulong itemID;
        public GameObject itemPrefab;
        public itemType itemType;
        public string itemName;
        [TextArea(15,20)]
        public string itemDescription;
        public ulong itemValue;
        public Sprite itemSprite;

    }
}
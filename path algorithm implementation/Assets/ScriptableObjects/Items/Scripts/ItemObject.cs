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

    // Parent class for all items. Contains general details that most items contain
    public abstract class ItemObject : ScriptableObject
    {
        public int itemID;
        public itemType itemType;
        public string itemName;
        [TextArea(15,20)]
        public string itemDescription;
        public ulong itemValue;
        public ulong itemAmount;
        public Sprite itemSprite;

    }

    [System.Serializable]
    public class Item
    {
        public int itemID;
        public string itemName;

        public Item(ItemObject item)
        {
            itemID = item.itemID;
            itemName = item.itemName;
        }

        public Item()
        {
            itemID = -1;
            itemName = "";
        }
    }
}
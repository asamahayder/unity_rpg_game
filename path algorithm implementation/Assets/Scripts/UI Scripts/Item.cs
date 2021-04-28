using UnityEngine;

namespace UI_Scripts
{
    public class Item
    {
        public enum ItemCategory
        {
            Equipment, 
            Consumable, 
            Quest, 
            Currency, 
            Resource, 
            Miscellaneous,
        }

        public Item(ulong itemID, ulong itemAmount, string itemName, string itemDescription, ulong itemValue, ItemCategory itemType, Sprite itemIcon)
        {
            this.ItemID = itemID;
            this.ItemAmount = itemAmount;
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
            this.ItemValue = itemValue;
            this.ItemType = itemType;
            this.ItemIcon = itemIcon;
        }

        public ulong ItemID { get; set; }

        public ulong ItemAmount { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public ulong ItemValue { get; set; }

        public ItemCategory ItemType { get; set; }

        public Sprite ItemIcon { get; set; }
    }
}
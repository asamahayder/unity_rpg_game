using System.Collections.Generic;
using UnityEngine;

namespace UI_Scripts
{
    public class ItemEquipment : Item
    {
        private Dictionary<string, int> _itemStats;
        private Dictionary<string, int> _itemLevelRequirements;

        public ItemEquipment(ulong itemID, ulong itemAmount, string itemName, string itemDescription, ulong itemValue, ItemCategory itemType, Sprite itemIcon, Dictionary<string, int> itemStats, Dictionary<string, int> itemLevelRequirements) : base(itemID, itemAmount, itemName, itemDescription, itemValue, itemType, itemIcon)
        {
            this._itemStats = itemStats;
            this._itemLevelRequirements = itemLevelRequirements;
        }
    }
}

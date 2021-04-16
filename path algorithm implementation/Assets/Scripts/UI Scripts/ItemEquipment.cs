using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipment : Item
{
    public Dictionary<string, int> itemStats;
    public Dictionary<string, int> itemLevelRequirements;

    public ItemEquipment(ulong itemID, ulong itemAmount, string itemName, string itemDescription, ulong itemValue, itenCategory itemType, Sprite itemIcon, Dictionary<string, int> itemStats, Dictionary<string, int> itemLevelRequirements) : base(itemID, itemAmount, itemName, itemDescription, itemValue, itemType, itemIcon)
    {
        this.itemStats = itemStats;
        this.itemLevelRequirements = itemLevelRequirements;
    }
}

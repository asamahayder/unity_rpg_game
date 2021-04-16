using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum itenCategory
    {
        equipment, 
        consumable, 
        quest, 
        currency, 
        resource, 
        miscellaneous,
    }
    
    public ulong itemID;
    public ulong itemAmount;
    public string itemName;
    public string itemDescription;
    public ulong itemValue;
    public itenCategory itemType;
    public Sprite itemIcon;

    public Item(ulong itemID, ulong itemAmount, string itemName, string itemDescription, ulong itemValue, itenCategory itemType, Sprite itemIcon)
    {
        this.itemID = itemID;
        this.itemAmount = itemAmount;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemValue = itemValue;
        this.itemType = itemType;
        this.itemIcon = itemIcon;
    }
}
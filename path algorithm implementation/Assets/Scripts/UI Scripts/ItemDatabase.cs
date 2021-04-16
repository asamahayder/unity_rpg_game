using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    public List<Item> databaseItems;

    public ItemDatabase()
    {
        buildDatabase();
    }

    void buildDatabase()
    {
        databaseItems = new List<Item>()
        {
            new ItemEquipment(1, 1, "Sword","A sword", 50, Item.itenCategory.equipment, null,null, null),
            new Item(0,100, "Coin","A coin", 1, Item.itenCategory.currency, null)
        };
    }

    public Item getItem(ulong id)
    {
        return databaseItems.Find(item => item.itemID == id);
    }
    
    public Item getItem(string name)
    {
        return databaseItems.Find(item => item.itemName == name);
    }
}

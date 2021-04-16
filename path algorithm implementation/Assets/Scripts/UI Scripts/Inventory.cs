using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void addItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> getItemList()
    {
        return itemList;
    }
}

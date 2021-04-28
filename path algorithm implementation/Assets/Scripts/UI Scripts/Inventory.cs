using System.Collections;
using System.Collections.Generic;
using UI_Scripts;
using UnityEngine;

public class Inventory
{
    
    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();
    }

    public void addItem(Item item)
    {
        _itemList.Add(item);
    }

    public List<Item> getItemList()
    {
        return _itemList;
    }

    public void setItemList(List<Item> itemList)
    {
        this._itemList = itemList;
    }
}

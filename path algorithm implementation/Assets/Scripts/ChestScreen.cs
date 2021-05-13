
using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestScreen : CharacterUserInterface
{
    public GameObject[] chestSlots;
    protected override void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventoryObject.inventory.inventoryItemList.Length; i++)
        {
            var obj = chestSlots[i];
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj);});
            displayedItems.Add(obj, inventoryObject.inventory.inventoryItemList[i]);
        }
    }

}

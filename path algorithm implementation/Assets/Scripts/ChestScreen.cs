
using System;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

// Handles the chest screen that pops up when opening a chest
public class ChestScreen : CharacterUserInterface
{
    public GameObject[] chestSlots;

    // Adds events to the itemSlots in the display and which items are in said slots
    protected override void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, DisplaySlot>();
        for (int i = 0; i < displayScreenContainer.inventory.itemList.Length; i++)
        {
            var obj = chestSlots[i];
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj);});
            displayedItems.Add(obj, displayScreenContainer.inventory.itemList[i]);
        }
    }

}

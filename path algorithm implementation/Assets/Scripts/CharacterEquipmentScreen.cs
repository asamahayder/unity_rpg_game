using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterEquipmentScreen : CharacterUserInterface
{

    public GameObject[] equipmentSlots;
    protected override void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventoryObject.inventory.inventoryItemList.Length; i++)
        {
            var obj = equipmentSlots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExit(obj);});
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj);});
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
            displayedItems.Add(obj, inventoryObject.inventory.inventoryItemList[i]);
        }
    }
}
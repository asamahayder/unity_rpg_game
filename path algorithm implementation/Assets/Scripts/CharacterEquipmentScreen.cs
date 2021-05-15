using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

// Implements the character user interface
public class CharacterEquipmentScreen : CharacterUserInterface
{

    // the equipment has a static amount of slots available defined in this array
    public GameObject[] equipmentSlots;
    
    // the implemented method from the interface where events are added to each displaySlot that is in the equipment
    protected override void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, DisplaySlot>();
        for (int i = 0; i < displayScreenContainer.inventory.itemList.Length; i++)
        {
            var obj = equipmentSlots[i];
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj);});
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExit(obj);});
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj);});
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
            displayedItems.Add(obj, displayScreenContainer.inventory.itemList[i]);
        }
    }
}
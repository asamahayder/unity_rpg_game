using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInventoryScreen : CharacterUserInterface
{
    public GameObject inventoryItemSlotPrefab;
    
    private int X_SPACE_BETWEEN_ITEMSLOTS = 90;
    private int Y_SPACE_BETWEEN_ITEMSLOTS = 60;
    private int X_START = -180;
    private float Y_START = 209.84f;
    private int NUMBER_OF_COLUMNS = 4;
        
    // Creates all the inventory slots for the inventory and instantiates them with a gameobject
    protected override void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, InventorySlot>();
        var inventorySlots = displayScreenContainer.inventory.inventoryItemList;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InstantiateInventorySlot(inventorySlots, i);
        }
    }
        
    //Instantiates the itemslot with its default sprite (basically just an empty inventory first time around with a transparent background indicating the slots)
    private void InstantiateInventorySlot(InventorySlot[] inventorySlots , int i)
    {
        GameObject obj = Instantiate(inventoryItemSlotPrefab, Vector3.zero,
            Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj);});
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExit(obj);});
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj);});
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
        displayedItems.Add(obj, inventorySlots[i]);
    }
    
    // Finds the position of the itemslot relative to where it's placed within its parent (the inventory) and other item slots
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMSLOTS * (i % NUMBER_OF_COLUMNS),
            Y_START - Y_SPACE_BETWEEN_ITEMSLOTS * (i / NUMBER_OF_COLUMNS),0f);
    }
}
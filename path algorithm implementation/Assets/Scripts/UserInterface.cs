using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour
{
    public Character character;
    public InventoryObject inventoryObject;
    protected Dictionary<GameObject, InventorySlot> displayedItems = new Dictionary<GameObject, InventorySlot>();
    
    void Start()
    {
        for (int i = 0; i < inventoryObject.inventory.inventoryItemList.Length; i++)
        {
            inventoryObject.inventory.inventoryItemList[i].parent = this;
        }
        CreateInventorySlots();
    }

    void Update()
    {
        UpdateDisplay();
    }
    protected abstract void CreateInventorySlots();

    // Visualizes the GUI for the inventory. If there is an item in a slot, loads the sprite of that item and its text field
    // if there isn't, the default transparent sprite is loaded instead
    // TODO this is an expensive method at the moment because it's getting called every frame
    // TODO should definitely separate the getting of components into another list of components and then looking it up with this method 
    private void UpdateDisplay()
    {
        foreach (var slot in displayedItems)
        {
            if (slot.Value.itemID >= 0)
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventoryObject.db.GetItem[slot.Value.itemID].itemSprite;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = slot.Value.itemAmount == 1 ? "" : slot.Value.itemAmount.ToString("n0");
            }
            else
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    //Creates an eventTrigger for the button on the itemSlot
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    // Checks what object we're hovering over. IF it's another object than the first one we started dragging, we update the mousedragger object
    protected void OnPointerEnter(GameObject obj)
    {
        character._mouseDragger.objOfHoveredOverItem = obj;
        if (displayedItems.ContainsKey(obj))
        {
            character._mouseDragger.itemHoveringOver = displayedItems[obj];
        }
    }

    // When finishing dragging, reset the mousedragger class to be null, so that we can start over again dragging a new item from beginDrag
    protected void OnPointerExit(GameObject obj)
    {
        character._mouseDragger.objOfHoveredOverItem = null;
        character._mouseDragger.itemHoveringOver = null;
    }

    // When hovering over an item and wanting to begin dragging it to another inventory slot, this code fires
    protected void OnDragBegin(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rectTransform = mouseObject .AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (displayedItems[obj].itemID != -1) {}
        {
            var newObjImage = mouseObject.AddComponent<Image>();
            newObjImage.sprite = inventoryObject.db.GetItem[displayedItems[obj].itemID].itemSprite;
            // makes it so that the image doesn't overlap the mouse cursor
            newObjImage.raycastTarget = false;
        }
        character._mouseDragger.objOfDraggedItem = mouseObject;
        character._mouseDragger.itemDragged = displayedItems[obj];
    }

    // When finished dragging an item, it either switches place with another item slot or nothing happens and the 
    // dragged item object is destroyed
    protected void OnDragEnd(GameObject obj)
    {
        var itemHoveringOver = character._mouseDragger.itemHoveringOver;
        var objHoveringOVer = character._mouseDragger.objOfHoveredOverItem;
        var getItem = inventoryObject.db.GetItem;
        
        if (objHoveringOVer)
        {
            // BIG BOY IF STATEMENT LOL
            // Checks if the item can be moved to a certain item slot or not depenging on its itemID. -1 is default for an empty item slot 0 and above is items.
            // there's also a method that checks if items can be placed on the equipment or not
            if (itemHoveringOver.isValidSlotPlacement(inventoryObject.db.GetItem[displayedItems[obj].itemID]) && (itemHoveringOver.item.itemID <= -1 || (itemHoveringOver.item.itemID >= 0 && displayedItems[obj].isValidSlotPlacement(getItem[itemHoveringOver.item.itemID]))))
            {
                InventoryObject.MoveItem(displayedItems[obj], itemHoveringOver.parent.displayedItems[objHoveringOVer]);
            }
            
        }
        // TODO could be used to drag an item off the screen to be dropped / deleted from inventory
        else
        {
            
        }

        Destroy(character._mouseDragger.objOfDraggedItem);
        character._mouseDragger.itemDragged = null;
    }

    // When dragging an item, the new dragged item object gets a copy of the sprite which was in the item slot and it 
    // moves with the mouse that's dragging it across the screen
    protected void OnDrag(GameObject obj)
    {
        if (character._mouseDragger.objOfDraggedItem != null)
        {
            character._mouseDragger.objOfDraggedItem.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    
}

// Contains the information regarding the currently dragged item and the item it's hovering over at the moment 
public class MouseDragger
{
    public GameObject objOfDraggedItem;
    public GameObject objOfHoveredOverItem;
    public InventorySlot itemDragged;
    public InventorySlot itemHoveringOver;
    
}

using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    private readonly MouseDragger _mouseDragger = new MouseDragger();
    
    public InventoryObject inventoryObject;
    public GameObject inventoryItemSlotPrefab;
    private int X_SPACE_BETWEEN_ITEMSLOTS = 90;
    private int Y_SPACE_BETWEEN_ITEMSLOTS = 60;
    private int X_START = -180;
    private float Y_START = 209.84f;
    private int NUMBER_OF_COLUMNS = 4;

    private Dictionary<GameObject, InventorySlot> displayedItems = new Dictionary<GameObject, InventorySlot>();
    
    void Start()
    {
        CreateInventorySlots();
    }

    void Update()
    {
        UpdateDisplay();
    }
    
    // Creates all the inventory slots for the inventory and instantiates them with a gameobject
    private void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, InventorySlot>();
        var inventorySlots = inventoryObject.inventory.inventoryItemList;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InstantiateInventorySlot(inventorySlots, i);
        }
    }

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
    
    //Creates an eventTrigger for the button on the itemSlot
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    // Checks what object we're hovering over. IF it's another object than the first one we started dragging, we update the mousedragger object
    private void OnPointerEnter(GameObject obj)
    {
        _mouseDragger.objOfHoveredOverItem = obj;
        if (displayedItems.ContainsKey(obj))
        {
            _mouseDragger.itemHoveringOver = displayedItems[obj];
        }
    }

    // When finishing dragging, reset the mousedragger class to be null, so that we can start over again dragging a new item from beginDrag
    private void OnPointerExit(GameObject obj)
    {
        _mouseDragger.objOfHoveredOverItem = null;
        _mouseDragger.itemHoveringOver = null;
    }

    // When hovering over an item and wanting to begin dragging it to another inventory slot, this code fires
    private void OnDragBegin(GameObject obj)
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
        _mouseDragger.objOfDraggedItem = mouseObject;
        _mouseDragger.itemDragged = displayedItems[obj];
    }

    // When finished dragging an item, it either switches place with another item slot or nothing happens and the 
    // dragged item object is destroyed
    private void OnDragEnd(GameObject obj)
    {
        if (_mouseDragger.objOfHoveredOverItem)
        {
            InventoryObject.MoveItem(displayedItems[obj], displayedItems[_mouseDragger.objOfHoveredOverItem]);
        }
        // TODO could be used to drag an item off the screen to be dropped / deleted from inventory
        else
        {
            
        }

        Destroy(_mouseDragger.objOfDraggedItem);
        _mouseDragger.itemDragged = null;
    }

    // When dragging an item, the new dragged item object gets a copy of the sprite which was in the item slot and it 
    // moves with the mouse that's dragging it across the screen
    private void OnDrag(GameObject obj)
    {
        if (_mouseDragger.objOfDraggedItem != null)
        {
            _mouseDragger.objOfDraggedItem.GetComponent<RectTransform>().position = Input.mousePosition;
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

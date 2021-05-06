using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public MouseDragger mouseDragger = new MouseDragger();
    
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

    private void CreateInventorySlots()
    {
        displayedItems = new Dictionary<GameObject, InventorySlot>();
        InventorySlot[] inventorySlots = inventoryObject.Inventory.inventoryItemList;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InstantiateInventorySlot(inventorySlots, i);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMSLOTS * (i % NUMBER_OF_COLUMNS),
            Y_START - Y_SPACE_BETWEEN_ITEMSLOTS * (i / NUMBER_OF_COLUMNS),0f);
    }

    void Update()
    {
        UpdateDisplay();
    }

    // Visualizes the GUI for the inventory. If there is an item in a slot, loads the sprite of that item and its text field
    // if there isn't, the default transparent sprite is loaded instead
    private void UpdateDisplay()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in displayedItems)
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
        var obj = Instantiate(inventoryItemSlotPrefab, Vector3.zero,
            Quaternion.identity, transform);
        //obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
        //    inventoryObject.db.GetItem[inventorySlots[i].item.itemID].itemSprite;
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        //obj.GetComponentInChildren<TextMeshProUGUI>().text =
        //    inventorySlots[i].itemAmount.ToString("n0");
        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnPointerEnter(obj);});
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnPointerExit(obj);});
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj);});
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj);});
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj);});
        displayedItems.Add(obj, inventorySlots[i]);
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
        mouseDragger.objOfHoveredOverItem = obj;
        if (displayedItems.ContainsKey(obj))
        {
            mouseDragger.itemHoveringOver = displayedItems[obj];
        }
    }

    // When finishing dragging, reset the mousedragger class to be null, so that we can start over again dragging a new item from beginDrag
    private void OnPointerExit(GameObject obj)
    {
        mouseDragger.objOfHoveredOverItem = null;
        mouseDragger.itemHoveringOver = null;
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
        mouseDragger.objOfDraggedItem = mouseObject;
        mouseDragger.itemDragged = displayedItems[obj];
    }

    private void OnDragEnd(GameObject obj)
    {
        if (mouseDragger.objOfHoveredOverItem)
        {
            inventoryObject.MoveItem(displayedItems[obj], displayedItems[mouseDragger.objOfHoveredOverItem]);
        }
        else
        {
            
        }

        Destroy(mouseDragger.objOfDraggedItem);
        mouseDragger.itemDragged = null;
    }

    private void OnDrag(GameObject obj)
    {
        if (mouseDragger.objOfDraggedItem != null)
        {
            mouseDragger.objOfDraggedItem.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    
}

public class MouseDragger
{
    public GameObject objOfDraggedItem;
    public GameObject objOfHoveredOverItem;
    public InventorySlot itemDragged;
    public InventorySlot itemHoveringOver;
    
}

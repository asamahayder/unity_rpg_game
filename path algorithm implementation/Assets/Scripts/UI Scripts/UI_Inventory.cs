using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform inventorySlotContainer;
    private Transform inventorySlotTemplate;

    private void Awake()
    {
        inventorySlotContainer = transform.Find("Inventory_Slot_Container");
        inventorySlotTemplate = inventorySlotContainer.Find("Inventory_Slot_Template");
    }

    public void setInventory(Inventory inventory)
    {
        this.inventory = inventory;
        refreshInventory();
    }

    private void refreshInventory()
    {
        int x = 0, y = 0;
        float itemSlotCellSizeWidth = 95f;
        float itemSlotCellSizeHeight = 70f;
        foreach (Item item in inventory.getItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(inventorySlotTemplate, inventorySlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector3(x * itemSlotCellSizeWidth, y * itemSlotCellSizeHeight);
            x++;
            if (x == 4)
            {
                x = 0;
                y--;
            }
        }
    }
}

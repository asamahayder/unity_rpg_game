using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;

public class InventoryScreen : MonoBehaviour
{
    public InventoryObject inventory;
    private int X_SPACE_BETWEEN_ITEMSLOTS = 90;
    private int Y_SPACE_BETWEEN_ITEMSLOTS = 60;
    private int X_START = -180;
    private float Y_START = 209.84f;
    private int NUMBER_OF_COLUMNS = 4;

    private Dictionary<InventorySlot, GameObject> displayedItems = new Dictionary<InventorySlot, GameObject>();
    
    void Start()
    {
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.inventoryItemList.Count; i++)
        {
            var obj = Instantiate(inventory.inventoryItemList[i].itemObject.itemPrefab, Vector3.zero,
                Quaternion.identity, transform);
            InstantiateInventorySlot(obj, i);
            displayedItems.Add(inventory.inventoryItemList[i], obj);
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

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.inventoryItemList.Count; i++)
        {
            if (displayedItems.ContainsKey(inventory.inventoryItemList[i]))
            {
                displayedItems[inventory.inventoryItemList[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.inventoryItemList[i].itemAmount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.inventoryItemList[i].itemObject.itemPrefab, Vector3.zero,
                    Quaternion.identity, transform);
                InstantiateInventorySlot(obj, i);
                displayedItems.Add(inventory.inventoryItemList[i], obj);
            }
        }
    }

    private void InstantiateInventorySlot(GameObject obj, int i)
    {
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        obj.GetComponentInChildren<TextMeshProUGUI>().text =
            inventory.inventoryItemList[i].itemAmount.ToString("n0");
    }
}

using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public GameObject inventoryPrefab;
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
        List<InventorySlot> inventorySlots = inventoryObject.Inventory.inventoryItemList;
        for (int i = 0; i < inventorySlots.Count; i++)
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

    private void UpdateDisplay()
    {
        List<InventorySlot> inventorySlots = inventoryObject.Inventory.inventoryItemList;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (displayedItems.ContainsKey(inventorySlots[i]))
            {
                displayedItems[inventorySlots[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventorySlots[i].itemAmount.ToString("n0");
            }
            else
            {
                InstantiateInventorySlot(inventorySlots, i);
            }
        }
    }

    private void InstantiateInventorySlot(List<InventorySlot> inventorySlots , int i)
    {
        var obj = Instantiate(inventoryPrefab, Vector3.zero,
            Quaternion.identity, transform);
        obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
            inventoryObject.db.GetItem[inventorySlots[i].item.itemID].itemSprite;
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        obj.GetComponentInChildren<TextMeshProUGUI>().text =
            inventorySlots[i].itemAmount.ToString("n0");
        displayedItems.Add(inventorySlots[i], obj);
    }
}

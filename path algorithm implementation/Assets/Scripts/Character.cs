using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    public InventoryObject playerInventory;

    


    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            playerInventory.addItem(item.itemObject,1);
            Destroy(other.gameObject);
        }
    }
    // FOR TESTING, DELETES INVENTORY ITEMS ATM
    private void OnApplicationQuit()
    {
        playerInventory.inventoryItemList.Clear();
    }
}

using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{
    public InventoryObject playerInventoryObject;

    private void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        //Debug.Log(item);
        Debug.Log(groundItem.itemObject.itemName);
        if (groundItem)
        {
            if (playerInventoryObject.addItem(groundItem.itemObject, groundItem.itemObject.itemAmount))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE PRESSED");
            playerInventoryObject.SaveDatabase();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("ENTER PRESSED");
            playerInventoryObject.LoadDatabase();
        }
    }
    
    private void OnApplicationQuit()
    {
        playerInventoryObject.Inventory.inventoryItemList = new InventorySlot[28];
    }
}
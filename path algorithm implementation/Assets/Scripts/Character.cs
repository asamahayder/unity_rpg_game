using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{
    public readonly MouseDragger _mouseDragger = new MouseDragger();
    public InventoryObject playerInventoryObject;

    private void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        //Debug.Log(item);
        Debug.Log(groundItem.itemObject.itemName);
        if (groundItem)
        {
            if (playerInventoryObject.AddItemToInventorySlot(groundItem.itemObject, groundItem.itemObject.itemAmount))
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
        playerInventoryObject.inventory.inventoryItemList = new InventorySlot[28];
    }
}
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{
    public InventoryObject playerInventoryObject;
    private TreeBehavior treeBehavior;

    public delegate void OnLogGathered();
    public static event OnLogGathered onLogGathered;

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

    private void Start()
    {
        treeBehavior = GameObject.Find("Tree").GetComponent<TreeBehavior>();
    }

    private void OnApplicationQuit()
    {
        playerInventoryObject.inventory.inventoryItemList = new InventorySlot[28];
    }

    public void collectLogs()
    {
        playerInventoryObject.AddItemToInventorySlot(treeBehavior.logs, 1);

        if (onLogGathered != null)
        {
            onLogGathered();
        }

    }
}
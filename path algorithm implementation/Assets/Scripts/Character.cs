using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{
    public readonly MouseDragger _mouseDragger = new MouseDragger();
    public InventoryObject inventory;
    public InventoryObject equipment;
    
    private TreeBehavior treeBehavior;
    public delegate void OnLogGathered();
    public static event OnLogGathered onLogGathered;

    private void OnTriggerEnter(Collider other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        //Debug.Log(item);
        Debug.Log(groundItem.itemObject.itemName);
        if (!groundItem) return;
        if (inventory.AddItemToInventorySlot(groundItem.itemObject, groundItem.itemObject.itemAmount))
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE PRESSED");
            inventory.SaveDatabase();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("BACKSPACE PRESSED");
            inventory.LoadDatabase();
        }
    }

    private void Start()
    {
        treeBehavior = GameObject.Find("Tree").GetComponent<TreeBehavior>();
    }

    private void OnApplicationQuit()
    {
        inventory.inventory.Clear();
        equipment.inventory.Clear();
    }

    public void collectLogs()
    {
        inventory.AddItemToInventorySlot(treeBehavior.logs, 1);

        if (onLogGathered != null)
        {
            onLogGathered();
        }

    }
}
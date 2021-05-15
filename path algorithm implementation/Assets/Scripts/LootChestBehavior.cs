using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

// Handles what happens when trying to interact with a dropped chest from an enemy. Implements the Interactable interface.
public class LootChestBehavior : Interactable
{
    public GameObject chestScreenPrefab;
    public DisplayScreenContainer chestContainer;
    private GameObject newChestScreen;
    private bool chestOpened = false;

    // Implemented method that defines what happens when interacting with the chest
    protected override void onInteract()
    {
        chestOpened = true;
        if (newChestScreen is { }) return;
        newChestScreen = Instantiate(chestScreenPrefab, new Vector3(-600,-365.53f, 0), transform.rotation) as GameObject;
        newChestScreen.transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform, false);
    }

    // Adds randomized items to the chest from the database
    private void AddItemsToChest()
    {
        var amountOfLoot = RandomRange(1, 8);
        for (var i = 0; i < amountOfLoot; i++)
        {
            var itemIdInChest = RandomRange(0, chestContainer.db.itemObjects.Length);
            var itemObject = chestContainer.db.GetItem[itemIdInChest];
            chestContainer.AddItemToInventorySlot(itemObject, itemObject.itemAmount);
        }
        
    }

    // Helper function to randomize how many itmes to get from the chest as well as which items
    private int RandomRange(int start, int end)
    {
        return Random.Range(start, end);
    }

    protected override void Start()
    {
        base.Start();
        chestContainer.inventory.Clear();
        AddItemsToChest();
    }

    // Destroys the chest after a certain amount of time
    protected override void Update()
    {
        base.Update();
        if (!chestOpened || (chestOpened && newChestScreen is null))
        {
            chestOpened = false;
            Destroy(this.gameObject, 10);
        }
    }

    protected override void onMouseOver()
    {
        
    }
}

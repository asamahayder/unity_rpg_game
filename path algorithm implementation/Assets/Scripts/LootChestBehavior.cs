using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory.Scripts;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

public class LootChestBehavior : Interactable
{
    public GameObject chestScreenPrefab;
    public InventoryObject chestInventory;
    private GameObject newChestScreen;
    private bool chestOpened = false;
    protected override void onInteract()
    {
        chestOpened = true;
        if (newChestScreen is { }) return;
        newChestScreen = Instantiate(chestScreenPrefab, new Vector3(-600,-365.53f, 0), transform.rotation) as GameObject;
        newChestScreen.transform.SetParent(GameObject.FindGameObjectWithTag("UI Canvas").transform, false);
    }

    private void AddItemsToChest()
    {
        var amountOfLoot = RandomRange(1, 8);
        for (var i = 0; i < amountOfLoot; i++)
        {
            var itemIdInChest = RandomRange(0, chestInventory.db.items.Length);
            var itemObject = chestInventory.db.GetItem[itemIdInChest];
            chestInventory.AddItemToInventorySlot(itemObject, itemObject.itemAmount);
        }
        
    }

    private int RandomRange(int start, int end)
    {
        return Random.Range(start, end);
    }

    protected override void Start()
    {
        base.Start();
        chestInventory.inventory.Clear();
        AddItemsToChest();
    }

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

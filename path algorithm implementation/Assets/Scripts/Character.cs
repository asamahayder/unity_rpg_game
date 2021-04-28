using System.Collections;
using System.Collections.Generic;
using UI_Scripts;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private UI_Inventory uiInventory;

    private Inventory inventory;
    private  ItemDatabase itemDatabase;

    void Awake()
    {
        inventory = new Inventory();
        itemDatabase = new ItemDatabase();
        inventory.setItemList(itemDatabase.getDatabase());
        uiInventory.SetInventory(inventory);
    }
}

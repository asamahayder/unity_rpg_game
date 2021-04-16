using System.Collections;
using System.Collections.Generic;
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
        inventory.addItem(itemDatabase.getItem(0));
        inventory.addItem(itemDatabase.getItem(1));
        inventory.addItem(itemDatabase.getItem(0));
        inventory.addItem(itemDatabase.getItem(0));
        inventory.addItem(itemDatabase.getItem(0));
        uiInventory.setInventory(inventory);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ScriptableObjects.Items.Scripts;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string databaseSaveDir;
        public ItemDatabase db;
        public Inventory Inventory;

        public bool addItem(ItemObject itemObject, ulong itemAmount)
        {
            Item item = new Item(itemObject);
            Debug.Log(item);
            
            if (CheckTypesOfItem(itemObject))
            {
                Debug.Log("item is Nonstackable");
                if (Inventory.inventoryItemList.Count < 28)
                {
                    Inventory.inventoryItemList.Add(new InventorySlot(item.itemID, item, itemAmount));
                    return true;
                }
            }

            for (int i = 0; i < Inventory.inventoryItemList.Count; i++)
            {
                if (Inventory.inventoryItemList[i].item.itemID == item.itemID)
                {
                    Inventory.inventoryItemList[i].addItemAmount(itemAmount);
                    return true;
                }
            }
            if (Inventory.inventoryItemList.Count < 28)
            {
                Inventory.inventoryItemList.Add(new InventorySlot(item.itemID, item, itemAmount));
            }
            return Inventory.inventoryItemList.Count < 28;
        }

        private bool CheckTypesOfItem(ItemObject itemObject)
        {
            return itemObject is ConsumableObject || itemObject is EquipmentObject ||
                   itemObject is MiscellaneousObject || itemObject is ResourceObject || itemObject is QuestObject;
        }

        [ContextMenu("Save")]
        public void SaveDatabase()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(string.Concat(Application.persistentDataPath, databaseSaveDir));
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        [ContextMenu("Load")]
        public void LoadDatabase()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, databaseSaveDir)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(string.Concat(Application.persistentDataPath, databaseSaveDir), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(),this);
                fs.Close();
            }
        }

        [ContextMenu("Clear Inventory")]
        public void ClearInventory()
        {
            Inventory = new Inventory();
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public ulong itemID;
        public Item item;
        public ulong itemAmount;

        public InventorySlot(ulong itemId, Item item, ulong itemAmount)
        {
            this.itemID = itemId;
            this.item = item;
            this.itemAmount = itemAmount;
        }

        public void addItemAmount(ulong amount)
        {
            itemAmount += amount;
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> inventoryItemList = new List<InventorySlot>();
    }
}

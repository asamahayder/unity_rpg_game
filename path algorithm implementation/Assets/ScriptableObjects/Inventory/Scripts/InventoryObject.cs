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
            if (CheckTypesOfItem(itemObject))
            {
                Debug.Log("item is Nonstackable");
                return findFirstEmptySlot(item, itemAmount, false) != null;
            }

            foreach (var slot in Inventory.inventoryItemList)
            {
                if (slot.itemID != item.itemID) continue;
                slot.addItemAmount(itemAmount);
                return true;
            }

            return findFirstEmptySlot(item, itemAmount, true) != null;
        }

        private InventorySlot findFirstEmptySlot(Item item, ulong itemAmount, bool stackable)
        {
            InventorySlot[] inventorySlots = Inventory.inventoryItemList;
            foreach (var slot in inventorySlots)
            {
                if (slot.itemID != -1) continue;
                slot.UpdateSlot(item.itemID, item, itemAmount);
                return slot;
            }
            return null;
        }

        private bool CheckTypesOfItem(ItemObject itemObject)
        {
            return itemObject is ConsumableObject || itemObject is EquipmentObject ||
                   itemObject is MiscellaneousObject || itemObject is ResourceObject || itemObject is QuestObject;
        }

        public void MoveItem(InventorySlot itemOne, InventorySlot itemTwo)
        {
            InventorySlot temp = new InventorySlot(itemTwo.itemID, itemTwo.item, itemTwo.itemAmount);
            itemTwo.UpdateSlot(itemOne.itemID, itemOne.item, itemOne.itemAmount);
            itemOne.UpdateSlot(temp.itemID, temp.item, temp.itemAmount);
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < Inventory.inventoryItemList.Length; i++)
            {
                if (Inventory.inventoryItemList[i].item == item)
                {
                    Inventory.inventoryItemList[i].UpdateSlot(-1, null, 0);
                }
            }
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
        public int itemID;
        public Item item;
        public ulong itemAmount;

        public InventorySlot(int itemId, Item item, ulong itemAmount)
        {
            this.itemID = itemId;
            this.item = item;
            this.itemAmount = itemAmount;
        }
        
        public InventorySlot()
        {
            this.itemID = -1;
            this.item = null;
            this.itemAmount = 0;
        }

        public void UpdateSlot(int itemId, Item item, ulong itemAmount)
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
        public InventorySlot[] inventoryItemList = new InventorySlot[28];
    }
}

using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public string databaseSaveDir;
        public ItemDatabase db;
        public Inventory inventory;

        // Adds items to a item slot in the inventory. If it is full, it will not add anymore items, unless it's a stackable item
        public bool AddItemToInventorySlot(ItemObject itemObject, ulong itemAmount)
        {
            var item = new Item(itemObject);
            if (CheckTypesOfItem(itemObject))
            {
                //Debug.Log("item is NonStackable");
                return FindFirstEmptySlot(item, itemAmount) != null;
            }
            foreach (var slot in inventory.inventoryItemList)
            {
                if (slot.itemID != item.itemID) continue;
                slot.addItemAmount(itemAmount);
                return true;
            }
            return FindFirstEmptySlot(item, itemAmount) != null;
        }

        // Iterates through the inventory to find an empty slot for the new item
        private InventorySlot FindFirstEmptySlot(Item item, ulong itemAmount)
        {
            var inventorySlots = inventory.inventoryItemList;
            foreach (var slot in inventorySlots)
            {
                if (slot.itemID != -1) continue;
                slot.UpdateSlot(item.itemID, item, itemAmount);
                return slot;
            }
            return null;
        }

        // Checks whether an item is stackable or not
        // TODO probably better to have an attribute for the item to indicate if it's stackable or not instead of doing the checking here
        private static bool CheckTypesOfItem(ItemObject itemObject)
        {
            return itemObject is ConsumableObject || itemObject is EquipmentObject ||
                   itemObject is MiscellaneousObject || itemObject is ResourceObject || itemObject is QuestObject;
        }

        // Moves an item from one inventory slot to another
        public static void MoveItem(InventorySlot itemOne, InventorySlot itemTwo)
        {
            var temp = new InventorySlot(itemTwo.itemID, itemTwo.item, itemTwo.itemAmount);
            itemTwo.UpdateSlot(itemOne.itemID, itemOne.item, itemOne.itemAmount);
            itemOne.UpdateSlot(temp.itemID, temp.item, temp.itemAmount);
        }

        // Removes item from the inventory
        // TODO implement a right click drop interface for the item (could also just implement drag to drop, already ready)
        public void RemoveItem(Item item)
        {
            foreach (var slotItem in inventory.inventoryItemList)
            {
                if (slotItem.item == item)
                {
                    slotItem.UpdateSlot(-1, null, 0);
                }
            }
        }

        // Saves this class's information to a JSON file.
        [ContextMenu("Save")]
        public void SaveDatabase()
        {
            var saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(string.Concat(Application.persistentDataPath, databaseSaveDir));
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        
        // Loads the class's information from the saved JSON file. If there isn't one to load, nothing is done
        [ContextMenu("Load")]
        public void LoadDatabase()
        {
            if (!File.Exists(string.Concat(Application.persistentDataPath, databaseSaveDir))) return;
            var bf = new BinaryFormatter();
            var fs = File.Open(string.Concat(Application.persistentDataPath, databaseSaveDir), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(fs).ToString(),this);
            fs.Close();
        }

        // An easy way to clear the inventory. Mostly used for testing purposes at the moment
        [ContextMenu("Clear Inventory")]
        public void ClearInventory()
        {
            inventory.Clear();
        }
    }

    // The class that contains all the information regarding a item's slot in the inventory
    [System.Serializable]
    public class InventorySlot
    {
        public UserInterface parent;
        public EquipmentType[] equipmentTypes = new EquipmentType[0];
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

        // Updates the current item in this slot with the new item given from the parameters
        public void UpdateSlot(int newItemId, Item newItem, ulong newItemAmount)
        {
            this.itemID = newItemId;
            this.item = newItem;
            this.itemAmount = newItemAmount;
        }

        // Adds to the item amount of the item in this item slot
        public void addItemAmount(ulong itemAmountToBeAdded)
        {
            this.itemAmount += itemAmountToBeAdded;
        }

        public bool isValidSlotPlacement(ItemObject itemObject)
        {
            if (equipmentTypes.Length <= 0)
            {
                return true;
            }

            if (itemObject is EquipmentObject equipmentObject)
            {
                return equipmentTypes.Any(type => equipmentObject.equipmentType == type);
            }
            return false;
        }
    }

    // The base inventory class which holds all the inventory slots for this inventory. It starts with being 28 empty item slots
    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] inventoryItemList = new InventorySlot[28];

        public void Clear()
        {
            for (int i = 0; i < inventoryItemList.Length; i++)
            {
                inventoryItemList[i].UpdateSlot(-1, new Item(), 0);
            }
        }
    }
}

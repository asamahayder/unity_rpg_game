using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    // Display Object that contains the list of items and its game objects
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class DisplayScreenContainer : ScriptableObject
    {
        public string databaseSaveDir;
        public ItemDatabase db;
        public Container inventory;

        // Adds items into the inventory if there is a valid place to be placed into
        public bool AddItemToInventorySlot(ItemObject itemObject, ulong itemAmount)
        {
            var item = new Item(itemObject);
            if (CheckTypesOfItem(itemObject))
            {
                return FindFirstEmptySlot(item, itemAmount) != null;
            }
            foreach (var slot in inventory.itemList)
            {
                if (slot.itemID != item.itemID) continue;
                slot.addItemAmount(itemAmount);
                return true;
            }
            return FindFirstEmptySlot(item, itemAmount) != null;
        }

        // Iterates through the inventory to find an empty slot for the new item
        private DisplaySlot FindFirstEmptySlot(Item item, ulong itemAmount)
        {
            var inventorySlots = inventory.itemList;
            foreach (var slot in inventorySlots)
            {
                if (slot.itemID != -1) continue;
                slot.UpdateSlot(item.itemID, item, itemAmount);
                return slot;
            }
            return null;
        }

        // Checks whether an item is stackable or not
        private static bool CheckTypesOfItem(ItemObject itemObject)
        {
            return itemObject is ConsumableObject || itemObject is EquipmentObject ||
                   itemObject is MiscellaneousObject || itemObject is ResourceObject || itemObject is QuestObject;
        }

        // Moves an item from one inventory slot to another
        public static void MoveItem(DisplaySlot itemOne, DisplaySlot itemTwo)
        {
            var temp = new DisplaySlot(itemTwo.itemID, itemTwo.item, itemTwo.itemAmount);
            itemTwo.UpdateSlot(itemOne.itemID, itemOne.item, itemOne.itemAmount);
            itemOne.UpdateSlot(temp.itemID, temp.item, temp.itemAmount);
        }

        // Removes a item from the inventory
        public void RemoveItem(Item item)
        {
            foreach (var slotItem in inventory.itemList)
            {
                if (slotItem.item == item)
                {
                    slotItem.UpdateSlot(-1, null, 0);
                }
            }
        }

        // Saves this class's information into a JSON file.
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

    // The class that contains all the information regarding a item's slot in the display
    [System.Serializable]
    public class DisplaySlot
    {
        public CharacterUserInterface parent;
        public EquipmentType[] equipmentTypes = new EquipmentType[0];
        public int itemID;
        public Item item;
        public ulong itemAmount;

        public DisplaySlot(int itemId, Item item, ulong itemAmount)
        {
            this.itemID = itemId;
            this.item = item;
            this.itemAmount = itemAmount;
        }
        
        // Empty constructor for creating empty slots
        public DisplaySlot()
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

        // Checks if the new item can be placed into the current slot
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

    // The base inventory class which holds all the inventory slots. It starts with being 28 empty item slots or 5 for the equipment "inventory"
    [System.Serializable]
    public class Container
    {
        public DisplaySlot[] itemList = new DisplaySlot[28];

        public void Clear()
        {
            foreach (var slot in itemList)
            {
                slot.UpdateSlot(-1, new Item(), 0);
            }
        }
    }
}

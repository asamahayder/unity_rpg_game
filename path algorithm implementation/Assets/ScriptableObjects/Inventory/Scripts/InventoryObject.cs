using System.Collections.Generic;
using ScriptableObjects.Items.Scripts;
using UnityEngine;

namespace ScriptableObjects.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public List<InventorySlot> inventoryItemList = new List<InventorySlot>();

        public void addItem(ItemObject itemObject, ulong itemAmount)
        {
            bool itemInInventroy = false;
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (inventoryItemList[i].itemObject == itemObject)
                {
                    inventoryItemList[i].addItemAmount(itemAmount);
                    itemInInventroy = true;
                    break;
                }
            }
            if (!itemInInventroy && inventoryItemList.Count < 28)
            {
                inventoryItemList.Add(new InventorySlot(itemObject, itemAmount));
            }
        }
    }
    
    [System.Serializable]
    public class InventorySlot
    {
        public ItemObject itemObject;
        public ulong itemAmount;

        public InventorySlot(ItemObject itemObject, ulong itemAmount)
        {
            this.itemObject = itemObject;
            this.itemAmount = itemAmount;
        }

        public void addItemAmount(ulong amount)
        {
            itemAmount += amount;
        }
    }
}

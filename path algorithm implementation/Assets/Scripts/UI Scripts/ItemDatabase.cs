using System.Collections.Generic;
using UnityEngine;

namespace UI_Scripts
{
    public class ItemDatabase
    {
        private List<Item> _databaseItems;

        public ItemDatabase()
        {
            BuildDatabase();
        }

        void BuildDatabase()
        {
            _databaseItems = new List<Item>()
            {
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(1, 1, "Sword","A sword", 50, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/ax2"),null, null),
                new ItemEquipment(2,1, "Axe","Axe", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/bow2"),null,null),
                new ItemEquipment(3,1, "Bow","Bow", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/sword2"),null,null),
                new ItemEquipment(2,1, "Staff","Staff", 1, Item.ItemCategory.Equipment, Resources.Load<Sprite>("Textures/staff2"),null,null)


            };
        }

        public Item getItem(ulong id)
        {
            return _databaseItems.Find(item => item.ItemID == id);
        }
    
        public Item getItem(string name)
        {
            return _databaseItems.Find(item => item.ItemName == name);
        }

        public List<Item> getDatabase()
        {
            return _databaseItems;
        }
    }
}

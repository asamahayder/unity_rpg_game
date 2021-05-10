using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum EquipmentType
    {
        Helmet,
        Chest,
        Weapon,
        Shield,
        Legs
    }
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public EquipmentType equipmentType;
        public int atkPower;
        public int defPower;
        public void Awake()
        {
            itemType = itemType.Equipment;
        }
    }
}
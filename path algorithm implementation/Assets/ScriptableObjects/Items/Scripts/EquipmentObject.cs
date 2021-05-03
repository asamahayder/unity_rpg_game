using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public int atkPower;
        public int defPower;
        public void Awake()
        {
            itemType = itemType.Equipment;
        }
    }
}
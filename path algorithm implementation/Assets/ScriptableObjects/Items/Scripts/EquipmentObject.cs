using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
    public class EquipmentObject : ItemObject
    {
        public Dictionary<string, int> equipmentStats;
        public Dictionary<string, int> equipmentLevelRequirements;
        public void Awake()
        {
            itemType = itemType.Equipment;
        }
    }
}

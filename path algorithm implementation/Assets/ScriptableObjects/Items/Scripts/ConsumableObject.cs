using System;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum ConsumableType
    {
        HealthRestore,
        StatsRestore,
        StatsBoost
    }
    [CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
    public class ConsumableObject : ItemObject
    {
        public ConsumableType consumableType;
        public int consumableRestoreAmount;
        private void Awake()
        {
            itemType = itemType.Consumable;
        }
    }
}
using System;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    // Miscellaneous item. Child of itemObject
    [CreateAssetMenu(fileName = "New Miscellaneous Object", menuName = "Inventory System/Items/Miscellaneous")]
    public class MiscellaneousObject : ItemObject
    {
        private void Awake()
        {
            itemType = itemType.Miscellaneous;
        }
    }
}
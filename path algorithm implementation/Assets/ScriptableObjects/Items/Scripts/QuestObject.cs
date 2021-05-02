using System;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Quest Object", menuName = "Inventory System/Items/Quest")]
    public class QuestObject : ItemObject
    {
        private void Awake()
        {
            itemType = itemType.Quest;
        }
    }
}

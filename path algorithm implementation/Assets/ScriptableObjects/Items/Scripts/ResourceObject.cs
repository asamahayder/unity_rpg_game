using System;
using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    public enum ResourceType
    {
        Woodcutting,
        Mining,
        Fishing
    }
    [CreateAssetMenu(fileName = "New Resource Object", menuName = "Inventory System/Items/Resource")]
    public class ResourceObject : ItemObject
    {
        public ResourceType resourceType;
        private void Awake()
        {
            itemType = itemType.Resource;
        }
    }
}
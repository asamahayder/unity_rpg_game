using UnityEngine;

namespace ScriptableObjects.Items.Scripts
{
    [CreateAssetMenu(fileName = "New Currency Object", menuName = "Inventory System/Items/Currency")]
    public class CurrencyObject : ItemObject
    {
        private void Awake()
        {
            itemType = itemType.Currency;
        }
    }
}

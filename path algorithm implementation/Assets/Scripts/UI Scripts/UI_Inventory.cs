using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class UI_Inventory : MonoBehaviour
    {
        private Inventory _inventory;
        private Transform _inventorySlotContainer;
        private Transform _inventorySlotTemplate;

        public GameObject itemSprite;

        private void Awake()
        {
            _inventorySlotContainer = transform.Find("Inventory_Slot_Container");
            _inventorySlotTemplate = _inventorySlotContainer.Find("Inventory_Slot_Template");
        }

        public void SetInventory(Inventory inventory)
        {
            this._inventory = inventory;
            RefreshInventory();
        }

        private void RefreshInventory()
        {
            int x = 0, y = 0;
            float itemSlotCellSizeWidth = 95f;
            float itemSlotCellSizeHeight = 70f;
            foreach (Item item in _inventory.getItemList())
            {
                Image image = itemSprite.GetComponent<Image>();
                image.sprite = item.ItemIcon;
                RectTransform itemSlotRectTransform = Instantiate(_inventorySlotTemplate, _inventorySlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector3(x * itemSlotCellSizeWidth, y * itemSlotCellSizeHeight);
                x++;
                if (x == 4)
                {
                    x = 0;
                    y--;
                }
            }
        }
    }
}

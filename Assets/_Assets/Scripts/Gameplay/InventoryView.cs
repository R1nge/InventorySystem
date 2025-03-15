using System;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private ulong id;
        [SerializeField] private InventoryItemPositionView[] positions;
        public UnityEvent<ItemData> OnItemAdded, OnItemRemoved;
        private Inventory _inventory;
        [Inject] private InventorySaver _inventorySaver;

        private void Awake()
        {
            _inventory = new Inventory(id, _inventorySaver);
            _inventory.Load();
        }

        public void AddItem(ItemView itemView)
        {
            SnapItem(itemView);
            var data = itemView.Item.ItemData;
            _inventory.AddItem(data);
            OnItemAdded?.Invoke(data);
        }

        private void SnapItem(ItemView itemView)
        {
            var itemType = itemView.Item.ItemData.type;
            var position = itemType switch
            {
                ItemType.Pickaxe => positions[0],
                ItemType.Lamp => positions[1],
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
            };
            position.Put(itemView);
        }

        public void RemoveItem(ItemData itemData)
        {
            UnSnapItem(itemData.type);
            _inventory.RemoveItem(itemData);
            OnItemRemoved?.Invoke(itemData);
        }

        private void UnSnapItem(ItemType itemType)
        {
            var position = itemType switch
            {
                ItemType.Pickaxe => positions[0],
                ItemType.Lamp => positions[1],
                _ => throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null)
            };
            position.Take();
        }
    }
}
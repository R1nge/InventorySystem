using System.Linq;
using _Assets.Scripts.Services.Api;
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
        [Inject] private Api _api;
        private Inventory _inventory;
        [Inject] private InventorySaver _inventorySaver;
        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventory = new Inventory(id, _inventorySaver, _api);
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
            var position = positions.FirstOrDefault(view => view.ItemType == itemType);
            if (position == null)
            {
                Debug.LogError($"No position for item type {itemType}");
                return;
            }

            position.Put(itemView);
        }

        public void RemoveItem(ItemView itemView)
        {
            var data = itemView.Item.ItemData;
            _inventory.RemoveItem(data);
            OnItemRemoved?.Invoke(data);
        }
    }
}
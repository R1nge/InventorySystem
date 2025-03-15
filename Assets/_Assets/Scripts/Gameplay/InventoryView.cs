using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private ulong id;
        public UnityEvent<ItemData> OnItemAdded, OnItemRemoved;
        private Inventory _inventory;
        [Inject] private InventorySaver _inventorySaver;

        private void Awake()
        {
            _inventory = new Inventory(id, _inventorySaver);
            _inventory.Load();
        }

        public void AddItem(ItemData itemData)
        {
            _inventory.AddItem(itemData);
            OnItemAdded?.Invoke(itemData);
        }

        private void RemoveItem(ItemData itemData)
        {
            _inventory.RemoveItem(itemData);
            OnItemRemoved?.Invoke(itemData);
        }
    }
}
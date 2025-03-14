using UnityEngine;
using UnityEngine.Events;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryView : MonoBehaviour
    {
        public UnityEvent<ItemData> OnItemAdded, OnItemRemoved;
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = new Inventory();
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
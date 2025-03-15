using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class Inventory
    {
        private readonly ulong _id;
        private readonly InventorySaver _inventorySaver;
        private readonly List<ItemData> _items;

        public Inventory(ulong id, InventorySaver inventorySaver)
        {
            _id = id;
            _inventorySaver = inventorySaver;
            _items = new List<ItemData>();
        }

        public void Load()
        {
            var save = _inventorySaver.Load(_id);

            if (save != null && save.items != null)
            {
                _items.AddRange(save.items);
            }

            for (int i = 0; i < _items.Count; i++)
            {
                Debug.Log($"Item loaded for id {_id} : {_items[i].title}");
            }
        }

        public void AddItem(ItemData itemData)
        {
            Debug.Log($"Item added: {itemData.title}");
            _items.Add(itemData);
            _inventorySaver.Save(this, _id);
        }

        public void RemoveItem(ItemData itemData)
        {
            Debug.Log($"Item removed: {itemData.title}");
            _items.Remove(itemData);
            _inventorySaver.Save(this, _id);
        }

        public List<ItemData> GetItems() => _items;
    }
}
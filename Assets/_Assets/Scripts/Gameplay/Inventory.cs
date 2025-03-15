using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class Inventory
    {
        private readonly List<ItemData> _items = new();

        public void AddItem(ItemData itemData)
        {
            Debug.Log($"Item added: {itemData.title}");
            _items.Add(itemData);
        }

        public void RemoveItem(ItemData itemData)
        {
            Debug.Log($"Item removed: {itemData.title}");
            _items.Remove(itemData);
        }
    }
}
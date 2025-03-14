using System.Collections.Generic;

namespace _Assets.Scripts.Gameplay
{
    public class Inventory
    {
        private readonly List<ItemData> _items = new();

        public void AddItem(ItemData itemData)
        {
            _items.Add(itemData);
        }

        public void RemoveItem(ItemData itemData)
        {
            _items.Remove(itemData);
        }
    }
}
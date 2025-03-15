using System;

namespace _Assets.Scripts.Gameplay
{
    [Serializable]
    public class ItemData : IComparable
    {
        public ulong id;
        public string title;
        public int weight;
        public ItemType type;

        public int CompareTo(object obj)
        {
            return id.CompareTo(((ItemData)obj).id);
        }
    }
}
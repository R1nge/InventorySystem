using System;
using System.Collections.Generic;

namespace _Assets.Scripts.Gameplay
{
    [Serializable]
    public class InventorySaveData
    {
        public ulong id;
        public List<ItemData> items;
    }
}
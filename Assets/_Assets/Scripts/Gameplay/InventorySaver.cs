using Newtonsoft.Json;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class InventorySaver
    {
        public void Save(Inventory inventory, ulong id)
        {
            var saveData = new InventorySaveData
            {
                id = id,
                items = inventory.GetItems()
            };

            PlayerPrefs.SetString($"Inventory_{id}", JsonConvert.SerializeObject(saveData));
            Debug.Log($"SAVING JSON {JsonConvert.SerializeObject(saveData)}");
            PlayerPrefs.Save();
        }

        public InventorySaveData Load(ulong id)
        {
            if (!PlayerPrefs.HasKey($"Inventory_{id}")) return new InventorySaveData();
            return JsonConvert.DeserializeObject<InventorySaveData>(PlayerPrefs.GetString($"Inventory_{id}"));
        }
    }
}
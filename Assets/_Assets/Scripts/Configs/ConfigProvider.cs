using System.Linq;
using _Assets.Scripts.Gameplay;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;

        [SerializeField] private ItemScriptableObject[] itemsData;
        public UIConfig UIConfig => uiConfig;

        public ItemScriptableObject GetItem(ulong id)
        {
            return itemsData.FirstOrDefault(item => item.ItemData.id == id);
        }
    }
}
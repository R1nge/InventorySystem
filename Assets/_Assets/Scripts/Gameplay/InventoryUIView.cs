using _Assets.Scripts.Configs;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryUIView : MonoBehaviour
    {
        [SerializeField] private GameObject ui;
        [SerializeField] private Transform slotChild;
        [SerializeField] private SlotView[] slots;
        [Inject] private ConfigProvider _configProvider;

        private void Awake()
        {
            Hide();
        }


        public void Show(Inventory inventory)
        {
            Clear();
            var array = inventory.GetItems().ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < slots.Length; j++)
                {
                    var item = array[i];
                    if (slots[j].ItemType != item.type) continue;
                    slots[j].SetIcon(_configProvider.GetItem(item.id).Icon);
                }
            }

            ui.SetActive(true);
        }

        public void Refresh(Inventory inventory)
        {
            Clear();
            var array = inventory.GetItems().ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < slots.Length; j++)
                {
                    var item = array[i];
                    if (slots[j].ItemType != item.type) continue;
                    slots[j].SetIcon(_configProvider.GetItem(item.id).Icon);
                }
            }
        }

        public void Hide()
        {
            ui.SetActive(false);
            Clear();
        }

        private void Clear()
        {
            foreach (var slot in slots)
            {
                slot.SetIcon(null);
            }
        }
    }
}
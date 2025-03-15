using System;
using _Assets.Scripts.Configs;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryUIView : MonoBehaviour
    {
        [SerializeField] private GameObject ui;
        [SerializeField] private Transform slotChild;
        [SerializeField] private SlotView slotPrefab;
        [Inject] private ConfigProvider _configProvider;

        private void Awake()
        {
            Hide();
        }


        public void Show(Inventory inventory)
        {
            Clear();
            foreach (var item in inventory.GetItems().ToArray())
            {
                var slot = Instantiate(slotPrefab, slotChild);
                slot.SetIcon(_configProvider.GetItem(item.id).Icon);
            }

            ui.SetActive(true);
        }

        public void Refresh(Inventory inventory)
        {
            Clear();
            foreach (var item in inventory.GetItems().ToArray())
            {
                var slot = Instantiate(slotPrefab, slotChild);
                slot.SetIcon(_configProvider.GetItem(item.id).Icon);
            }
        }

        public void Hide()
        {
            ui.SetActive(false);
            Clear();
        }

        private void Clear()
        {
            foreach (Transform child in slotChild)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
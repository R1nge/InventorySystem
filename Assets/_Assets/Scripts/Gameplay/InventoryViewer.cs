using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryViewer : MonoBehaviour
    {
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private InventoryUIView inventoryUIView;

        private void Awake()
        {
            inventoryView.OnItemAdded.AddListener(Refresh);
            inventoryView.OnItemRemoved.AddListener(Refresh);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryUIView.Show(inventoryView.Inventory);
            }
        }

        private void OnDestroy()
        {
            inventoryView.OnItemAdded.RemoveListener(Refresh);
            inventoryView.OnItemRemoved.RemoveListener(Refresh);
        }

        private void Refresh(ItemData arg0)
        {
            inventoryUIView.Refresh(inventoryView.Inventory);
        }
    }
}
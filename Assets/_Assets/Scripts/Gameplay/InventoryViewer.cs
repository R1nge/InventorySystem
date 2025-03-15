using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryViewer : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private InventoryUIView inventoryUIView;
        private InventoryView _lastInventory;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.transform.TryGetComponent(out InventoryView inventoryView))
                    {
                        _lastInventory = inventoryView;
                        inventoryView.OnItemAdded.AddListener(Refresh);
                        inventoryView.OnItemRemoved.AddListener(Refresh);
                        inventoryUIView.Show(inventoryView.Inventory);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_lastInventory == null) return;
                _lastInventory.OnItemAdded.RemoveListener(Refresh);
                _lastInventory.OnItemRemoved.RemoveListener(Refresh);
                inventoryUIView.Hide();
                _lastInventory = null;
            }
        }

        private void Refresh(ItemData arg0)
        {
            if (_lastInventory == null)
            {
                Debug.LogError("Trying to refresh inventory when it is null");
                return;
            }

            inventoryUIView.Refresh(_lastInventory.Inventory);
        }
    }
}
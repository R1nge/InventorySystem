using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class ItemInteractor : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float zOffset = 10;
        private ItemView _currentItem;
        private Plane _dragPlane;
        private Vector3 _startDragOffset;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryPickUpItem();
            }

            if (Input.GetMouseButton(0) && _currentItem != null)
            {
                Drag();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_currentItem != null)
                {
                    if (!TryAddToInventory(_currentItem))
                    {
                        Drop();
                    }
                }
            }
        }

        private bool TryPickUpItem()
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.TryGetComponent(out ItemView itemView))
                {
                    _currentItem = itemView;
                    var distanceBetweenCameraAndItem = new Vector3(_currentItem.transform.position.x,
                        _currentItem.transform.position.y, camera.transform.position.z + zOffset);
                    _dragPlane = new Plane(Vector3.forward, distanceBetweenCameraAndItem);
                    Vector3 hitPoint = hit.point;
                    _startDragOffset = _currentItem.transform.position - hitPoint;
                    itemView.DisableGravity();
                    return true;
                }
            }

            return false;
        }

        private void Drop()
        {
            _currentItem.EnableGravity();
            _currentItem = null;
        }

        private void Drag()
        {
            if (_currentItem != null)
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (_dragPlane.Raycast(ray, out var enter))
                {
                    var hitPoint = ray.GetPoint(enter);
                    _currentItem.transform.position = hitPoint + _startDragOffset;
                }
            }
        }

        private bool TryAddToInventory(ItemView itemView)
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.TryGetComponent(out InventoryView inventoryView))
                {
                    inventoryView.AddItem(itemView.Item.ItemData);
                    return true;
                }
            }

            return false;
        }
    }
}
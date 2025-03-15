using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class ItemInteractor : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float zOffset = 10;
        private readonly RaycastHit[] _hits = new RaycastHit[10];
        private ItemView _currentItem;
        private Plane _dragPlane;
        private Vector3 _startDragOffset;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!TryTakeFromInventory())
                {
                    TryPickUpItem();
                }
            }

            if (_currentItem != null)
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
                else
                {
                    TryTakeFromInventory();
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
            _currentItem.ResetVelocity();
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
            var hits = Physics.RaycastNonAlloc(ray, _hits, Mathf.Infinity);

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    if (_hits[i].transform.TryGetComponent(out InventoryView inventoryView))
                    {
                        inventoryView.AddItem(itemView);
                        _currentItem = null;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryTakeFromInventory()
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastNonAlloc(ray, _hits, Mathf.Infinity);

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    if (_hits[i].transform.TryGetComponent(out InventoryItemPositionView inventoryItemPositionView))
                    {
                        _currentItem = inventoryItemPositionView.Take(Input.mousePosition);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
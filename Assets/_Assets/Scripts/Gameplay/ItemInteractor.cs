using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class ItemInteractor : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        private ItemView _currentItem;
        private Vector3 _itemStartPosition;
        private Vector3 _startDragOffset;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.transform.TryGetComponent(out ItemView itemView))
                    {
                        _currentItem = itemView;
                        _itemStartPosition = itemView.transform.position;
                        _startDragOffset = _itemStartPosition - ScreenToWorld(Input.mousePosition);
                        _startDragOffset.z = 0;
                        itemView.DisableGravity();
                    }
                }
            }

            Drag();

            if (Input.GetMouseButtonUp(0))
            {
                if (_currentItem != null)
                {
                    _currentItem.EnableGravity();
                    _currentItem = null;
                }
            }
        }

        private void Drag()
        {
            if (_currentItem != null)
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = camera.transform.position.z + _currentItem.transform.position.z;
                var mouseWorldPosition = ScreenToWorld(mousePosition);
                mouseWorldPosition.z = _currentItem.transform.position.z;

                _currentItem.transform.position = mouseWorldPosition + _startDragOffset;

                Debug.Log($"Current: {mousePosition}, World: {mouseWorldPosition}, Offset: {_startDragOffset}");
            }
        }

        private Vector3 ScreenToWorld(Vector3 screenPosition)
        {
            screenPosition.z = camera.WorldToScreenPoint(_currentItem.transform.position).z;
            return camera.ScreenToWorldPoint(screenPosition);
        }
    }
}
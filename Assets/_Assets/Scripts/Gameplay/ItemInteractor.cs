using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class ItemInteractor : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        private ItemView _currentItem;
        private Plane _dragPlane;
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
                        _dragPlane = new Plane(Vector3.forward, _currentItem.transform.position);
                        Vector3 hitPoint = hit.point;
                        _startDragOffset = _currentItem.transform.position - hitPoint;
                        itemView.DisableGravity();
                    }
                }
            }

            if (Input.GetMouseButton(0) && _currentItem != null)
            {
                Drag();
            }

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
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (_dragPlane.Raycast(ray, out var enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    _currentItem.transform.position = hitPoint + _startDragOffset;

                    Debug.Log($"Ray: {ray}, Hit Point: {hitPoint}, Offset: {_startDragOffset}");
                }
            }
        }
    }
}
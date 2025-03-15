using System.Collections;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryItemPositionView : MonoBehaviour
    {
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private float lerpDuration = 0.15f;
        [SerializeField] private Transform position;
        private ItemView _currentItem;

        public void Put(ItemView itemView)
        {
            if (_currentItem != null)
            {
                Debug.LogError("Item position is already occupied", this);
                return;
            }

            _currentItem = itemView;
            itemView.DisableGravity();
            itemView.EnableKinematic();
            StartCoroutine(LerpToPosition(itemView, lerpDuration, position.position));
            itemView.transform.SetParent(position);
        }

        public ItemView Take(Vector3 mousePosition)
        {
            if (_currentItem != null)
            {
                var item = _currentItem;
                _currentItem.ResetVelocity();
                _currentItem.EnableGravity();
                _currentItem.DisableKinematic();
                _currentItem.transform.SetParent(null);
                StartCoroutine(LerpToPosition(item, lerpDuration, mousePosition));
                inventoryView.RemoveItem(_currentItem);
                _currentItem = null;
                return item;
            }

            Debug.LogError("Trying to take an item from an empty position", this);

            return null;
        }

        private IEnumerator LerpToPosition(ItemView itemView, float duration, Vector3 endPosition)
        {
            var startPosition = itemView.transform.position;
            var t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                itemView.transform.position = Vector3.Lerp(startPosition, endPosition, t / duration);
                yield return null;
            }

            itemView.transform.position = endPosition;
        }
    }
}
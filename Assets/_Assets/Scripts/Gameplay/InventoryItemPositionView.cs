using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class InventoryItemPositionView : MonoBehaviour
    {
        [SerializeField] private Transform position;
        private ItemView _currentItem;

        public void Put(ItemView itemView)
        {
            _currentItem = itemView;
            itemView.DisableGravity();
            itemView.EnableKinematic();
            itemView.transform.parent = position;
            itemView.transform.localPosition = Vector3.zero;
            itemView.transform.localRotation = Quaternion.identity;
        }

        public ItemView Take()
        {
            if (_currentItem != null)
            {
                var item = _currentItem;
                _currentItem.EnableGravity();
                _currentItem.DisableKinematic();
                _currentItem.transform.parent = null;
                _currentItem = null;
                return item;
            }

            Debug.LogError("Trying to take an item from an empty position", this);

            return null;
        }
    }
}
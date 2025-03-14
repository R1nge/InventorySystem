using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private ItemScriptableObject item;
        [SerializeField] private new Rigidbody rigidbody;
        private float _defaultGravityScale;
        public ItemScriptableObject Item => item;


        public void DisableGravity()
        {
            rigidbody.useGravity = false;
        }

        public void EnableGravity()
        {
            rigidbody.useGravity = true;
        }
    }
}
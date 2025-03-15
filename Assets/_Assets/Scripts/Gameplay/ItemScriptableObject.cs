using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemData itemData;
        public Sprite Icon => icon;
        public ItemData ItemData => itemData;
    }
}
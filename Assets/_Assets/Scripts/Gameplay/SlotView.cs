using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Gameplay
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Image icon;
        public ItemType ItemType => itemType;

        public void SetIcon(Sprite sprite) => icon.sprite = sprite;
    }
}
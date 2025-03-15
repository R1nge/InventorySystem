using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Gameplay
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private Image icon;

        public void SetIcon(Sprite sprite) => icon.sprite = sprite;
    }
}
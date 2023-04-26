using UnityEngine;
using System.Collections.Generic;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "Body", menuName = "Details/Body")]
    public class Body : Detail
    {
        [SerializeField] private Sprite platformSprite;
        [SerializeField] private Sprite sideColoringSprite;
        [SerializeField] private List<Color> colors;
        [SerializeField] private int coloringPrice;
        [SerializeField] private string nameInPlayerPrefs;
        private int currentColorIndex = 0;

        public Sprite PlatformSprite { get => platformSprite; }
        public Sprite SideColoringSprite { get => sideColoringSprite; }
        public List<Color> Colors { get => colors; }
        public int ColoringPrice { get => coloringPrice; }
        public string NameInPlayerPrefs { get => nameInPlayerPrefs; }
        public int CurrentColorIndex { get => currentColorIndex; }

        private void Awake()
        {
            currentColorIndex = PlayerPrefs.GetInt(nameInPlayerPrefs, 0);
        }

        public void ChangeColor(int newIndex)
        {
            currentColorIndex = newIndex;
            PlayerPrefs.SetInt(nameInPlayerPrefs, currentColorIndex);
        }

        public Color GetCurrentColor()
        {
            return Colors[currentColorIndex];
        }
    }
}

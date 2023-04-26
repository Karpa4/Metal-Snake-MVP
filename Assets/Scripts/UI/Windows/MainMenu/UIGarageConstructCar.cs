using UnityEngine;
using UnityEngine.UI;
using Game.PlayerConstructor;
using System.Collections.Generic;

namespace UI.Windows.Garage
{
    public class UIGarageConstructCar : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> constructImages;
        [SerializeField] private SpriteRenderer backWheelsImage;
        [SerializeField] private SpriteRenderer coloringBodyImage;
        [SerializeField] private SpriteRenderer secondStandImage;
        [SerializeField] private GameObject imagesGO;

        private Vector2 backWhellsStartPos;
        private Vector2 secondStandStartPos;
        private List<Vector2> startPositions;

        public static UIGarageConstructCar instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            SaveStartPositions();
        }

        /// <summary>
        /// �������� ����������� ������
        /// </summary>
        public void OpenImages()
        {
            imagesGO.SetActive(true);
        }

        /// <summary>
        /// ��������� ����������� ������
        /// </summary>
        public void CloseImages()
        {
            imagesGO.SetActive(false);
        }

        /// <summary>
        /// ��������� ��������� ������������ ��� ����� � �����
        /// </summary>
        /// <param name="sprites">�������</param>
        /// <param name="offsets">��������� ������� ��������</param>
        public void StartConstruct(List<Sprite> sprites, List<Vector2> offsets)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                ChangeSprite(i, sprites[i], offsets[i]);
            }
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="newColor">���� ������</param>
        public void ChangeColor(Color newColor)
        {
            coloringBodyImage.color = newColor;
        }

        /// <summary>
        /// ��������� ������� ������ 2 ������
        /// </summary>
        /// <param name="secondStandSprite">������</param>
        public void ChangeSecondStandSprite(Sprite secondStandSprite)
        {
            secondStandImage.sprite = secondStandSprite;
        }

        /// <summary>
        /// ��������� ������ �������
        /// </summary>
        /// <param name="detailType">DetailType, ��������������� � int</param>
        /// <param name="detailSprite">������</param>
        /// <param name="offset">��������� �������</param>
        public void ChangeSprite(int detailType, Sprite detailSprite, Vector2 offset)
        {
            constructImages[detailType].sprite = detailSprite;
            constructImages[detailType].transform.position = startPositions[detailType] + offset;
            if (detailType == 3)
            {
                backWheelsImage.transform.position = backWhellsStartPos + offset;
                backWheelsImage.sprite = detailSprite;
            }
        }

        /// <summary>
        /// ���������� ��������� ������� ���� ��������
        /// </summary>
        private void SaveStartPositions()
        {
            backWhellsStartPos = backWheelsImage.transform.position;
            secondStandStartPos = secondStandImage.transform.position;
            startPositions = new List<Vector2>();
            foreach (var item in constructImages)
            {
                startPositions.Add(item.transform.position);
            }
        }
    }
}

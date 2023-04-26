using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Game.PlayerConstructor;
using System.Collections;

namespace UI.Windows.Garage
{
    public class UIDetailsInfo : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI characteristicsText;
        [SerializeField] private List<Button> detailButtons;
        [SerializeField] private List<Image> detailImages;
        [SerializeField] private List<Image> frameImages; 
        [SerializeField] private List<Image> paintSprite;
        [SerializeField] private Sprite spriteForColoring;
        [SerializeField] private Sprite enabledFrameImage; 
        [SerializeField] private Sprite disabledFrameImage; 
        private int currentIndex = 0;
        private DetailType currentType;
        private const float Modifier = 0.2f;
        private int activeButtonsCount = 0;
        private List<Detail> currentDetails;
        private List<Color> colors = new List<Color>();

        public event Action<DetailType, int> onDetailChanged;

        public void ShowAllDetails(List<Detail> details, DetailType type, int currentDetailIndex)
        {
            if (currentType == DetailType.Color)
            {
                for (int i = 0; i < activeButtonsCount; i++)
                {
                    detailImages[i].color = Color.white;
                }
            }

            ActiveButtonBeforeNewCategory();

            currentType = type;
            currentIndex = currentDetailIndex;
            currentDetails = details;

            int newDetailsCount = details.Count;
            for (int i = 0; i < newDetailsCount; i++)
            {
                if (!detailButtons[i].gameObject.activeSelf)
                {
                    detailButtons[i].gameObject.SetActive(true);

                }
                detailImages[i].sprite = details[i].GarageDetailSprite;
                detailImages[i].rectTransform.sizeDelta = new Vector2(290f, 290f);

            }

            detailButtons[currentIndex].interactable = false;
            ShowDetailInfo(details[currentIndex]);

            DeactiveExtraButtons(newDetailsCount);
            StartCoroutine(ChangeScrollRectContentPosition());
        }

        private IEnumerator ChangeScrollRectContentPosition()
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (activeButtonsCount > 3)
            {
                if (currentIndex > 1)
                {
                    scrollRect.horizontalNormalizedPosition = (float)currentIndex / activeButtonsCount + Modifier;
                }
                else
                {
                    scrollRect.horizontalNormalizedPosition = 0;
                }
            }
        }

        private void ShowDetailInfo(Detail currentDetail)
        {
            nameText.text = currentDetail.Name;
            descriptionText.text = currentDetail.Description;
            string detailCharacteristics = currentDetail.GetDetailCharacteristics();
            characteristicsText.text = detailCharacteristics;
        }

        public void ShowColors(List<Color> colors, int currentColorIndex)
        {
            this.colors = colors;
            ClearAllText();
            ActiveButtonBeforeNewCategory();
            currentType = DetailType.Color;
            currentIndex = currentColorIndex;

            int newDetailsCount = colors.Count;
            for (int i = 0; i < newDetailsCount; i++)
            {
                if (!detailButtons[i].gameObject.activeSelf)
                {
                    detailButtons[i].gameObject.SetActive(true);
                }
                detailImages[i].sprite = spriteForColoring;
                detailImages[i].rectTransform.sizeDelta = new Vector2(220f, 220f); 
                paintSprite[i].enabled = true;
                paintSprite[i].color = colors[i];
                if (i == currentIndex)
                {
                    detailButtons[i].interactable = false;
                }
            }

            paintSprite[0].color = new Color(0.48f, 0.22f, 0.1f); // По дефолту - коричневый цвет кузова

            DeactiveExtraButtons(newDetailsCount);
            StartCoroutine(ChangeScrollRectContentPosition());
        }

        private void DeactiveExtraButtons(int newDetailsCount)
        {
            if (newDetailsCount < activeButtonsCount)
            {
                for (int i = newDetailsCount; i < activeButtonsCount; i++)
                {
                    detailButtons[i].gameObject.SetActive(false);
                }
            }
            activeButtonsCount = newDetailsCount;
        }

        private void ActiveButtonBeforeNewCategory()
        {
            if (!detailButtons[currentIndex].interactable)
            {
                detailButtons[currentIndex].interactable = true;
            }
        }

        private void ClearAllText()
        {
            characteristicsText.text = string.Empty;
            descriptionText.text = string.Empty;
            nameText.text = string.Empty;
        }

        public void ChangeDetail(int newIndex)
        {
            detailButtons[currentIndex].interactable = true;
            frameImages[currentIndex].sprite = disabledFrameImage; 
            currentIndex = newIndex;
            detailButtons[currentIndex].interactable = false;
            frameImages[currentIndex].sprite = enabledFrameImage; 
            if (currentType == DetailType.Color)
            {
                UIGarageConstructCar.instance.ChangeColor(colors[currentIndex]);
            }
            else
            {
                ShowDetailInfo(currentDetails[currentIndex]);
                UIGarageConstructCar.instance.ChangeSprite((int)currentType, currentDetails[currentIndex].GarageDetailSprite, currentDetails[currentIndex].GarageOffset);
                if (currentType == DetailType.SecondWeapon)
                {
                    SecondWeapon secondWeapon = (SecondWeapon)currentDetails[currentIndex];
                    UIGarageConstructCar.instance.ChangeSecondStandSprite(secondWeapon.SecondStandUISprite);
                }
            }
            onDetailChanged?.Invoke(currentType, currentIndex);
        }
     
        public void SetButtonFrameSprite() // Меняем рамку выбранной детали
        {
            for (int i = 0; i < detailButtons.Count; i++)
            {
                if (detailButtons[i].interactable == false)
                {
                    frameImages[i].sprite = enabledFrameImage;
                }

                else
                {
                    frameImages[i].sprite = disabledFrameImage;
                }
            }
        }
    }
}

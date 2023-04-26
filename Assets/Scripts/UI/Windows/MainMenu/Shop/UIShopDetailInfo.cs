using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.PlayerConstructor;
using UI.Windows.Garage;

namespace UI.Windows.Shop
{
    public class UIShopDetailInfo : MonoBehaviour
    {
        [SerializeField] private Image detailImage;
        [SerializeField] private Image detailTruckBackground; // new
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI characteristicsText;
        [SerializeField] private Image currencyImage;
        [SerializeField] private TextMeshProUGUI priceText;

        [SerializeField] private Button closeButton;
        //[SerializeField] private Button areaCloseButton;

        private Vector3 detailSpriteOffset; // new
        private Vector3 truckSpriteOffset; // new

        private void Start()
        {
            closeButton.onClick.AddListener(CloseDetailInfo);
            //areaCloseButton.onClick.AddListener(CloseDetailInfo);
        }

        public void Construct(Detail detail)
        {
            detailImage.sprite = detail.GarageDetailSprite;
            detailTruckBackground.sprite = detail.ShopInfoTruckSprite; // new
            detailSpriteOffset = detail.ShopInfoDetailOffset; // new
            truckSpriteOffset = detail.ShopInfoTruckOffset; // new
            detailImage.transform.position += detailSpriteOffset; // new           
            detailTruckBackground.transform.position += truckSpriteOffset; // new
            detailImage.rectTransform.sizeDelta = detail.DetailImageSize; // new
            detailTruckBackground.rectTransform.sizeDelta = detail.TruckImageSize; // new

            nameText.text = detail.Name;
            descriptionText.text = detail.Description;
            priceText.text = detail.Price.ToString();
            if (detail.Currency == CurrencyType.Coins)
            {
                currencyImage.sprite = CurrencyImages.CoinsSprite;
            }
            else
            {
                currencyImage.sprite = CurrencyImages.MicrochipSprite;
            }
            characteristicsText.text = detail.GetDetailCharacteristics();
            gameObject.SetActive(true);
        }

        public void CloseDetailInfo()
        {
            gameObject.SetActive(false);
        }

        public void CleanUp()
        {
            closeButton.onClick.RemoveListener(CloseDetailInfo);
            //areaCloseButton.onClick.RemoveListener(CloseDetailInfo);
        }

        private void OnDisable()
        {
            detailImage.transform.position -= detailSpriteOffset; // new           
            detailTruckBackground.transform.position -= truckSpriteOffset; // new
        }
    }
}

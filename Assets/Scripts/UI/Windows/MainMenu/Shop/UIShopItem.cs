using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Services.PlayerData;

namespace UI.Windows.Shop
{
    public class UIShopItem : MonoBehaviour
    {
        [SerializeField] private Image currencyImage;
        [SerializeField] private Image detailImage;
        [SerializeField] private TextMeshProUGUI minLevelText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button button;

        private int detailIndex = 0;
        private UIShopCategory uiShopCategory;

        public int DetailPrice { get; private set; }
        public int DetailMinLevel { get; private set; }
        public CurrencyType Currency { get; private set; }

        public void Construct(Sprite detailImage, int detailMinLevel, int detailPrice, CurrencyType currency)
        {
            this.detailImage.sprite = detailImage;
            DetailPrice = detailPrice;
            DetailMinLevel = detailMinLevel;
            Currency = currency;
            
            minLevelText.text = detailMinLevel.ToString();
            priceText.text = detailPrice.ToString();
            if (currency == CurrencyType.Coins)
            {
                currencyImage.sprite = CurrencyImages.CoinsSprite;
            }
            else
            {
                currencyImage.sprite = CurrencyImages.MicrochipSprite;
            }
        }

        public void SetCategory(UIShopCategory uiShopCategory)
        {
            this.uiShopCategory = uiShopCategory;
            button.onClick.AddListener(DetailButtonClick);
        }

        public void SetNewDetailIndex(int newDetailIndex)
        {
            detailIndex = newDetailIndex;
        }

        public void CleanUp()
        {
            button.onClick.RemoveListener(DetailButtonClick);
        }

        public void DetailButtonClick()
        {
            uiShopCategory.ShowDetailInfo(detailIndex);
        }
    }
}

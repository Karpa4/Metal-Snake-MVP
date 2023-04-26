using UnityEngine;
using UnityEngine.UI;
using UI.Windows.MainMenu;

namespace UI.Windows.Shop
{

    public class ShopCellColor : MonoBehaviour
    {
        [SerializeField] private UIShopItem UIShopItemScript;
        [SerializeField] private Image frameImage;
        private UIShopWindow UIShopWindowScript;
        private UIMainMenuTopPlayerInfo topInfoScript;
        private int detailPrice;
        private int detailMinLevel;
        private Color redColor;
        private Color greenColor;
        
        private void Awake()
        {
            redColor = new Color(0.8f, 0.2f, 0.12f);
            greenColor = new Color(0.35f, 0.8f, 0.12f);            
            UIShopWindowScript = GetComponentInParent<UIShopWindow>();
            topInfoScript = FindObjectOfType<UIMainMenuTopPlayerInfo>();
        }

        private void OnEnable()
        {
            topInfoScript.PlayerStaticData.PlayerMoneyData.MoneyChangedEvent += SetFrameColor;
        }

        private void OnDisable()
        {
            topInfoScript.PlayerStaticData.PlayerMoneyData.MoneyChangedEvent -= SetFrameColor;
        }

        private void Start()
        {
            detailPrice = UIShopItemScript.DetailPrice;
            detailMinLevel = UIShopItemScript.DetailMinLevel;
            SetFrameColor(1);
        }

        private void SetFrameColor(int n)
        {

            if (UIShopItemScript.Currency == CurrencyType.Coins)
            {
                if (UIShopWindowScript.CheckPrice(CurrencyType.Coins, detailPrice) && UIShopWindowScript.CheckMinLevel(detailMinLevel))
                    frameImage.color = greenColor;

                else
                    frameImage.color = redColor;
            }

            else
            {
                if (UIShopWindowScript.CheckPrice(CurrencyType.Microchip, detailPrice) && UIShopWindowScript.CheckMinLevel(detailMinLevel))
                    frameImage.color = greenColor;

                else
                    frameImage.color = redColor;
            }
        }

        
    }
}

using Services.WindowsService;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Services.PlayerData;
using StaticData.Windows;
using Services.Sound;
using Game.PlayerConstructor;

namespace UI.Windows.Shop
{
    public class UIShopWindow : BaseWindow
    {
        [SerializeField] private Button backButton;
        [SerializeField] private UIShopDetailInfo shopDetailInfo;
        [SerializeField] private Button buyButton;
        [SerializeField] private List<UIShopCategory> uiShopCategories;

        private IWindowsService windowsService;
        private PlayerStaticData playerStaticData;
        private PlayerPartsData playerPartsData;
        private ButtonSounds buttonSoundsScript;
        private int currentCategoryIndex = 0;
        private int currentDetailIndex = 0;

        private void Awake()
        {
            buttonSoundsScript = FindObjectOfType<ButtonSounds>();
        }

        public void Construct(IWindowsService windowsService, PlayerStaticData playerStaticData, PlayerPartsData playerPartsData)
        {
            this.playerPartsData = playerPartsData;
            this.windowsService = windowsService;
            this.playerStaticData = playerStaticData;
            backButton.onClick.AddListener(OpenMainMenu);
            buyButton.onClick.AddListener(BuyDetail);
            ConstructShopCategories();
        }

        private void ConstructShopCategories()
        {
            for (int i = 0; i < playerPartsData.AllDetails.Count; i++)
            {
                if (playerPartsData.AllDetails[i].Count == 0)
                {
                    uiShopCategories[i].gameObject.SetActive(false);
                }
                else
                {
                    uiShopCategories[i].Construct(playerPartsData.AllDetails[i], i);
                }
            }
        }

        private void OpenMainMenu()
        {
            buttonSoundsScript.OnClick();
            windowsService.Open(WindowsID.MainMenu);
            CleanUp();
            Destroy(gameObject);
        }

        private void BuyDetail()
        {
            Detail newPlayerDetail = playerPartsData.AllDetails[currentCategoryIndex][currentDetailIndex];
            playerPartsData.PlayerDetails[currentCategoryIndex].Add(newPlayerDetail);
            playerPartsData.AllDetails[currentCategoryIndex].RemoveAt(currentDetailIndex);
            shopDetailInfo.CloseDetailInfo();
            playerPartsData.SavePlayerParts();
            if (newPlayerDetail.Currency == CurrencyType.Coins)
            {
                playerStaticData.PlayerMoneyData.SpendMoney(newPlayerDetail.Price);
            }
            else
            {
                playerStaticData.PlayerPremiumMoneyData.SpendMoney(newPlayerDetail.Price);
            }
            uiShopCategories[currentCategoryIndex].DeleteDetailItem(currentDetailIndex);
        }

        public void ShowDetailInfo(int categoryIndex, int detailIndex)
        {
            Detail detail = playerPartsData.AllDetails[categoryIndex][detailIndex];
            if (CheckMinLevel(detail.MinPlayerLevel) && CheckPrice(detail.Currency, detail.Price))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
            shopDetailInfo.Construct(detail);
            currentCategoryIndex = categoryIndex;
            currentDetailIndex = detailIndex;
        }

        public bool CheckMinLevel(int detailMinLevel)
        {
            return playerStaticData.PlayerLevelData.CurrentLevel >= detailMinLevel;
        }

        public bool CheckPrice(CurrencyType currency, int price) 
        {
            if (currency == CurrencyType.Coins)
            {
                return playerStaticData.PlayerMoneyData.MoneyCount >= price;
            }
            else
            {
                return playerStaticData.PlayerPremiumMoneyData.MoneyCount >= price;
            }
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            foreach (UIShopCategory category in uiShopCategories)
            {
                category.CleanUp();
            }
            shopDetailInfo.CleanUp();
            backButton.onClick.RemoveListener(OpenMainMenu);
            buyButton.onClick.RemoveListener(BuyDetail);
            Destroy(gameObject);
        }
    }
}

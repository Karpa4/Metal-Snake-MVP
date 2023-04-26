using System.Collections.Generic;
using UnityEngine;
using Game.PlayerConstructor;

namespace UI.Windows.Shop
{
    public class UIShopCategory : MonoBehaviour
    {
        [SerializeField] private UIShopItem shopItemPrefab;
        [SerializeField] private UIShopWindow uiShopWindow;
        [SerializeField] private Transform pointForShopItems;
        private int categoryIndex = 0;

        private List<UIShopItem> uiShopItems = new List<UIShopItem>();

        public void Construct(List<Detail> details, int categoryIndex)
        {
            for (int i = 0; i < details.Count; i++)
            {
                UIShopItem item = Instantiate(shopItemPrefab, pointForShopItems);
                item.Construct(details[i].GarageDetailSprite, details[i].MinPlayerLevel, details[i].Price, details[i].Currency);
                item.SetCategory(this);
                item.SetNewDetailIndex(i);                
                uiShopItems.Add(item);
                this.categoryIndex = categoryIndex;
            }
        }

        public void DeleteDetailItem(int detailIndex)
        {
            uiShopItems[detailIndex].CleanUp();
            Destroy(uiShopItems[detailIndex].gameObject);
            uiShopItems.RemoveAt(detailIndex);
            RefreshIndexesAfterBuying(detailIndex);
        }

        private void RefreshIndexesAfterBuying(int startIndex)
        {
            if (uiShopItems.Count == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                for (int i = startIndex; i < uiShopItems.Count; i++)
                {
                    uiShopItems[i].SetNewDetailIndex(i);
                }
            }
        }

        public void ShowDetailInfo(int detailIndex)
        {
            uiShopWindow.ShowDetailInfo(categoryIndex, detailIndex);
        }

        public void CleanUp()
        {
            foreach (UIShopItem item in uiShopItems)
            {
                item.CleanUp();
            }
        }
    }
}

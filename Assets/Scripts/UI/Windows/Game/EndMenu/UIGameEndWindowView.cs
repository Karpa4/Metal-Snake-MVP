using UnityEngine;
using TMPro;

namespace UI.Windows.Game.EndMenu
{
    public class UIGameEndWindowView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CarriagesCountText;
        [SerializeField] private TextMeshProUGUI expCountReward;
        [SerializeField] private TextMeshProUGUI moneyCountReward;

        public void DisplayStatistic(int carriagesCount, int rewardExpCount, int rewardMoneyCount)
        {
            CarriagesCountText.text = "Количество прицепов: " + carriagesCount;
            expCountReward.text = "Получено опыта: " + rewardExpCount;
            moneyCountReward.text = "Получено денег: " + rewardMoneyCount;
        }
    }
}
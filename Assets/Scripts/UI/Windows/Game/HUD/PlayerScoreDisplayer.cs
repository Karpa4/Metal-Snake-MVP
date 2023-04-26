using UnityEngine;
using TMPro;

namespace UI.Windows.Game.HUD
{
    public class PlayerScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void DisplayScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
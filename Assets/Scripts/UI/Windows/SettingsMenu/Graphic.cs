using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Windows.SettingsMenu
{
    public class Graphic : MonoBehaviour
    {
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private TMP_Dropdown qualityDropdown;

        public const string QualityConst = "Quality";
        public const string FullscreenConst = "Fullscreen";

        private void Start()
        {
            Application.targetFrameRate = 120;

            if (PlayerPrefs.HasKey(QualityConst))
            {
                SetQuality(PlayerPrefs.GetInt(QualityConst));
                if (qualityDropdown != null) qualityDropdown.value = (PlayerPrefs.GetInt(QualityConst));
            }
            else
            {
                SetQuality(2);
                if (qualityDropdown != null) qualityDropdown.SetValueWithoutNotify(2);
            }
            if (PlayerPrefs.HasKey(FullscreenConst))
            {
                SetFullscreen(PlayerPrefs.GetInt(FullscreenConst) == 1 ? true : false);
                if (fullscreenToggle != null) fullscreenToggle.isOn = (PlayerPrefs.GetInt(FullscreenConst) == 1 ? true : false);
            }
            else
            {
                SetFullscreen(true);
                if (fullscreenToggle != null) fullscreenToggle.SetIsOnWithoutNotify(true);
            }
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            PlayerPrefs.SetInt(QualityConst, qualityIndex);
        }

        public void SetFullscreen(bool mode)
        {
            Screen.fullScreen = mode;
            PlayerPrefs.SetInt(FullscreenConst, mode ? 1 : 0);
        }
    }
}

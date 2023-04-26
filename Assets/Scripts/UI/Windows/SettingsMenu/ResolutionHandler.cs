using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Windows.SettingsMenu
{
    public class ResolutionHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;


        Resolution[] resolutions;

        public const string ResolConst = "Resolution";

        private void Start()
        {
            if (resolutionDropdown != null)
            {
                resolutions = Screen.resolutions;
                resolutionDropdown.ClearOptions();
                List<string> options = new List<string>();

                for (int i = 0; i < resolutions.Length; i++)
                {
                    string option = resolutions[i].width + "x" + resolutions[i].height + "Hz" + resolutions[i].refreshRate;
                    options.Add(option);
                }
                resolutionDropdown.AddOptions(options);
                int resoIndex;
                if (PlayerPrefs.HasKey(ResolConst))
                {
                    resoIndex = PlayerPrefs.GetInt(ResolConst);
                    resolutionDropdown.value = (resoIndex);
                }
                else
                {
                    for (int i = 0; i < resolutions.Length; i++)
                    {
                        if (resolutions[i].width == Screen.currentResolution.width & resolutions[i].height == Screen.currentResolution.height)
                        {
                            resoIndex = i;
                            PlayerPrefs.SetInt(ResolConst, i);
                            resolutionDropdown.value = (resoIndex);
                        }
                    }
                }
            }
        }

        public void SetResolution(int ResolutionIndex)
        {
            Resolution resolution = resolutions[ResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            PlayerPrefs.SetInt(ResolConst, ResolutionIndex);
        }
    }
}

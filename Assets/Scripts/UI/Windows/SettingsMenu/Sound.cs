using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Windows.SettingsMenu
{
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey(ConstantVariables.EffectsVolume))
            {
                if (effectsSlider != null)
                {
                    effectsSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(ConstantVariables.EffectsVolumeSlider));
                }
            }
            else
            {
                if (effectsSlider != null)
                {
                    effectsSlider.SetValueWithoutNotify(effectsSlider.maxValue);
                }
            }
            if (PlayerPrefs.HasKey(ConstantVariables.MusicVolume))
            {
                if (musicSlider != null)
                {
                    musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat(ConstantVariables.MusicVolumeSlider));
                }
            }
            else
            {
                if (musicSlider != null)
                {
                    musicSlider.SetValueWithoutNotify(musicSlider.maxValue);
                }
            }
        }

        public void SetMusic(float Music)
        {
            float result = Mathf.Log10(Music) * 20;
            PlayerPrefs.SetFloat(ConstantVariables.MusicVolumeSlider, Music);
            audioMixer.SetFloat(ConstantVariables.MusicVolume, result);
            PlayerPrefs.SetFloat(ConstantVariables.MusicVolume, result);
        }

        public void SetEffects(float Effects)
        {
            float result = Mathf.Log10(Effects) * 20;
            PlayerPrefs.SetFloat(ConstantVariables.EffectsVolumeSlider, Effects);
            audioMixer.SetFloat(ConstantVariables.EffectsVolume, result);
            PlayerPrefs.SetFloat(ConstantVariables.EffectsVolume, result);
            audioMixer.SetFloat(ConstantVariables.IncreasedVolume, result);
            PlayerPrefs.SetFloat(ConstantVariables.IncreasedVolume, result);
        }
    }
}

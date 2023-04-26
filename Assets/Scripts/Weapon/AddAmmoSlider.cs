using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class AddAmmoSlider : MonoBehaviour
    {
        private AdditionalWeapon addWeaponScript;
        private Slider slider;
        private float timeBetweenFire;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            addWeaponScript = FindObjectOfType<AdditionalWeapon>();
        }

        void Start()
        {
            timeBetweenFire = addWeaponScript.GetTimeBetweenFire();
            slider.maxValue = 1f; 
        }

        void Update()
        {
            if (addWeaponScript.bullets > 0)
            {
                if (addWeaponScript.isReloaded)
                {
                    slider.value = slider.maxValue; // белый цвет спрайта

                }

                else
                {
                    slider.value = (timeBetweenFire - addWeaponScript.timeToReload) / timeBetweenFire;
                }
            }

            else
            {
                slider.value = 0;
            }

        }
    }
}

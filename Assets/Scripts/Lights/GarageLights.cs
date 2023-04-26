using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageLights : MonoBehaviour
{
    [SerializeField] private GameObject garageLights;
    [SerializeField] private GameObject screenLights;
    private bool garageLighted;

    public void LightSwitcher()
    {
        if (!garageLighted)
        {
            garageLighted = true;
            screenLights.SetActive(false);
            garageLights.SetActive(true);
        }

        else
        {
            garageLighted = false;
            garageLights.SetActive(false);
            screenLights.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    private Slider slider;
    private float minPos = -40f;
    private float maxPos = 540f;
    private GameObject mainPlayer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        mainPlayer = FindObjectOfType<MainPlayer>().gameObject;
        slider.minValue = minPos;
        slider.maxValue = maxPos;
    }

    private void Update()
    {
        slider.value = mainPlayer.transform.position.y;
    }
}

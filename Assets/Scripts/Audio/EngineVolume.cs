using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineVolume : MonoBehaviour
{
    [SerializeField] private float minEngineVolume = 0.2f;
    [SerializeField] private float maxEngineVolume = 0.8f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource engineSource;
    private CarDriver carDriver;
    
    void Start()
    {
        carDriver = GetComponent<CarDriver>();
    }

    void Update()
    {
        // “екуща€ скорость по шкале от 0 до 1 (отношение текущей скорости к максимальной)
        float currentSpeedPercent = carDriver.GetSpeed() / carDriver.carData.SpeedMax;

        // ѕрив€зываем громкость двигател€ к текущей скорости, ограничива€ еЄ произвольными пределами
        engineSource.volume = Mathf.Clamp(currentSpeedPercent, minEngineVolume, maxEngineVolume); 
    }

}

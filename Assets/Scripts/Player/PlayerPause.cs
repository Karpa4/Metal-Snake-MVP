using UnityEngine;
using Game;

public class PlayerPause : CarPause
{
    [SerializeField] private MainPlayer mainPlayer;
    [SerializeField] private BaseWeapon additionalWeapon;
    [SerializeField] private CarriageManager carriageManager;
    [SerializeField] private AudioSource mainThemeSource;
    [SerializeField] private float mainThemePauseVolume;

    private float defaultMainThemeVolume = 0;

    protected override void PauseOnExtend()
    {
        mainPlayer.enabled = false;
        carriageManager.SwitchCarriages(false);
        mainThemeSource.volume = mainThemePauseVolume;
        mainPlayer.PauseOn();
    }

    protected override void PauseOffExtend()
    {
        mainPlayer.enabled = true;
        carriageManager.SwitchCarriages(true);
        mainThemeSource.volume = defaultMainThemeVolume;
        mainPlayer.PauseOff();
    }

    protected override void StartExtend()
    {
        defaultMainThemeVolume = mainThemeSource.volume;
        additionalWeapon.SetPauseManager(RefContainer.Instance.MainPauseManager);
    }
}

using Services.Pause;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public abstract void SetPauseManager(PauseManager pauseManager);
}

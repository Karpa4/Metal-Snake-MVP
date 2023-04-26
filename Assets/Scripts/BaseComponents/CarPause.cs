using UnityEngine;
using Services.Pause;

public abstract class CarPause : MonoBehaviour, IPause, ISetWeapon
{
    [SerializeField] private CarDriver carDriver;
    [SerializeField] private EngineVolume engineVolume;
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private Rigidbody2D carRigidBody;
    [SerializeField] private AudioSource engineSource;
    [SerializeField] private DustParticle dustParticle;
    private PauseManager pauseManager;

    protected virtual void Start()
    {
        pauseManager = RefContainer.Instance.MainPauseManager;
        if (weapon != null)
        {
            weapon.SetPauseManager(pauseManager);
        }
        pauseManager.Register(this);
        StartExtend();
    }

    private void OnDestroy()
    {
        if (pauseManager != null)
        {
            pauseManager.Unregister(this);
        }
    }

    public void PauseOff()
    {
        carRigidBody.bodyType = RigidbodyType2D.Dynamic;
        dustParticle.PauseOff();
        PauseOffExtend();
        carDriver.enabled = true;
        engineVolume.enabled = true;
        engineSource.Play();
    }

    public void PauseOn()
    {
        carRigidBody.bodyType = RigidbodyType2D.Static;
        dustParticle.PauseOn();
        PauseOnExtend();
        carDriver.enabled = false;
        engineVolume.enabled = false;
        engineSource.Pause();
    }

    protected abstract void PauseOnExtend();
    protected abstract void PauseOffExtend();
    protected abstract void StartExtend();

    public void SetMainWeapon(BaseRotateWeapon weapon)
    {
        this.weapon = weapon;
        if (pauseManager != null)
        {
            weapon.SetPauseManager(pauseManager);
        }
    }
}

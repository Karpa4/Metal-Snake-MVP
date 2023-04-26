using UnityEngine;
using Services.Pause;

public class DustParticle : MonoBehaviour, IPause
{
    [SerializeField] private ParticleSystem[] dustParticles;

    private CarDriver carDriverScript;
    private MainPlayer mainPlayer;
    private Rigidbody2D rb;
    private ParticleSystem.EmissionModule[] emissions;
    private int emitNumber;
    private float speedRate;
    private float maxSpeed;
    private float mass;

    public void PauseOff()
    {
        foreach (ParticleSystem particle in dustParticles)
        {
            particle.Play(true);
        }
    }

    public void PauseOn()
    {
        foreach (ParticleSystem particle in dustParticles)
        {
            particle.Pause(true);
        }
    }

    private void Awake()
    {
        emissions = new ParticleSystem.EmissionModule[dustParticles.Length];
        for (int i = 0; i < dustParticles.Length; i++)
        {
            emissions[i] = dustParticles[i].emission;
        }

        if (transform.parent.CompareTag("Carriage"))
        {
            mainPlayer = FindObjectOfType<MainPlayer>();
            rb = mainPlayer.GetComponent<Rigidbody2D>();
            carDriverScript = mainPlayer.GetComponent<CarDriver>();
        }
        else
        {
            rb = GetComponentInParent<Rigidbody2D>();
            carDriverScript = GetComponentInParent<CarDriver>();
        }
    }

    void Start()
    {
        mass = rb.mass;
        maxSpeed = carDriverScript.GetMaxSpeed();
    }

    void Update()
    {
        if (mainPlayer != null)
        {
            speedRate = carDriverScript.GetSpeed() / maxSpeed;
            emitNumber = (int)(mass * speedRate);
            for (int i = 0; i < emissions.Length; i++)
            {
                emissions[i].rateOverDistance = emitNumber;
            }
        }
    }
}

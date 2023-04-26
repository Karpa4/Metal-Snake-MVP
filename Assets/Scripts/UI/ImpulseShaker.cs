using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpulseShaker : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private ExplosionController explosionController;

    private void Awake()
    {
        explosionController = FindObjectOfType<ExplosionController>();
        explosionController.ExplodeNow += () => impulseSource.GenerateImpulse();
    }
}

using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    [SerializeField] private RocketProjectile rocket;
    private AudioManager audioManager;
    private ExplosionController explosion;
    private AudioSource explosionSource;

    private void Start()
    {
        explosion = FindObjectOfType<ExplosionController>();
        audioManager = FindObjectOfType<AudioManager>();
        explosionSource = FindObjectOfType<MainPlayer>().ExplosionSource;
        rocket.BeforeDestroy += PlaySoundAndParticles;
    }

    private void PlaySoundAndParticles()
    {
        rocket.BeforeDestroy -= PlaySoundAndParticles;
        audioManager.PlaySound(AudioManager.Sound.Secondary101_hit, explosionSource);
        explosion.LightExplosion(transform.position);
    }
}

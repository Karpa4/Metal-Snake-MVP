using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StaticData.Sounds;

namespace Services.Sound
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundService : MonoBehaviour
    {
        public SoundStaticData soundStaticData;
        public AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            soundStaticData = Resources.Load<SoundStaticData>(ConstantVariables.SoundsPath);
        }
    }
}



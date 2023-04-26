using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Sound
{
    public class MenuStaticSounds : SoundService
    {
        public static MenuStaticSounds staticSounds;

        private void Start()
        {
            if (staticSounds == null) staticSounds = this;
        }

        public void PlaySound(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}


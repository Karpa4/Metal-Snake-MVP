using StaticData.Sounds;
using UnityEngine;
using Game.Player;

namespace Services.Sound
{

    public class PlayerSound : SoundService
    {
        private AudioClip audioClip;

        public void PlaySound(SoundID id)
        {
            foreach (SoundInstantiateData item in soundStaticData.instantiateDatas)
            {
                if (item.ID == id)
                {
                    if (item.ID != SoundID.Silence) audioSource.PlayOneShot(item.audioClip);
                    break;
                }
            }
        }
    }
}


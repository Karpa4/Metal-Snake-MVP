using UnityEngine;
using System;

namespace StaticData.Sounds
{
    [Serializable]
    public struct SoundInstantiateData
    {
        public SoundID ID;
        public AudioClip audioClip;
    }
}


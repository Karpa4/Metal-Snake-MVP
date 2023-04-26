using UnityEngine;

namespace StaticData.Sounds
{
    [CreateAssetMenu(fileName = "SoundsStaticData", menuName = "StaticData/Sound/Create Sound Static Data", order = 52)]
    public class SoundStaticData : ScriptableObject
    {
        public SoundInstantiateData[] instantiateDatas;
    }
}




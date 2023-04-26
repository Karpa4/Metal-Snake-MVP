using UnityEngine;

namespace Services.Sound
{
    public class ButtonSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip onClickSound;

        public void OnClick()
        {
            MenuStaticSounds.staticSounds.PlaySound(onClickSound);
        }
    }
}


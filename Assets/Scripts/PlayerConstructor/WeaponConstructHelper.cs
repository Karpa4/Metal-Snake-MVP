using UnityEngine;

namespace Game.PlayerConstructor
{
    public class WeaponConstructHelper : MonoBehaviour
    {
        [SerializeField] private Transform secondWeaponTransform;
        [SerializeField] private Transform secondStandTransform;

        public Transform SecondStandTransform { get => secondStandTransform; }
        public Transform SecondWeaponTransform { get => secondWeaponTransform; }

        public void DestroyUseless()
        {
            Destroy(secondStandTransform.gameObject);
            Destroy(secondWeaponTransform.gameObject);
            Destroy(GetComponent<WeaponConstructHelper>());
        }
    }
}

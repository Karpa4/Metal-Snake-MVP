using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

namespace Game
{
    public class CarriageFactory : MonoBehaviour
    {
        [SerializeField] private List<CarriagePickup> allCarriages;
        [SerializeField] private List<Sprite> carriageSprites;

        private void Start()
        {
            int maxIndex = carriageSprites.Count;
            foreach (CarriagePickup carPick in allCarriages)
            {
                carPick.Construct(carriageSprites[Random.Range(0, maxIndex)]);
            }
            RefContainer.Instance.MainPlayer.GetComponent<CarriageManager>().AddCarriage(carriageSprites[Random.Range(0, maxIndex)]);
        }
    }
}

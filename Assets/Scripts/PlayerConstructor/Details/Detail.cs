using UnityEngine;

namespace Game.PlayerConstructor
{
    [CreateAssetMenu(fileName = "New Detail", menuName = "Details/Detail")]
    public class Detail : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string detailGameName;
        [SerializeField] private string description;
        [SerializeField] private DetailType type;
        [SerializeField] private int price;
        [SerializeField] private CurrencyType currencyType;
        [SerializeField] private int minPlayerLevel;
        [SerializeField] private Sprite garageSprite;
        [SerializeField] private Sprite gameDetailSprite;
        [SerializeField] private Vector2 garageOffset;

        [SerializeField] private Sprite shopInfoTruckSprite; // new
        [SerializeField] private Vector3 shopInfoDetailOffset; // new
        [SerializeField] private Vector3 shopInfoTruckOffset; // new
        [SerializeField] private Vector2 detailImageSize; // new
        [SerializeField] private Vector2 truckImageSize; // new


        public int ID { get => id; }
        public string Name { get => detailGameName; }
        public string Description { get => description; }
        public DetailType Type { get => type; }
        public int Price { get => price; }
        public CurrencyType Currency { get => currencyType; }
        public int MinPlayerLevel { get => minPlayerLevel; }
        public Sprite GarageDetailSprite { get => garageSprite; }

        public Sprite ShopInfoTruckSprite { get => shopInfoTruckSprite; } // new
        public Vector3 ShopInfoDetailOffset { get => shopInfoDetailOffset; } // new
        public Vector3 ShopInfoTruckOffset { get => shopInfoTruckOffset; } // new
        public Vector2 DetailImageSize { get => detailImageSize; } // new
        public Vector2 TruckImageSize { get => truckImageSize; } // new

        public Sprite GameDetailSprite { get => gameDetailSprite; }
        public Vector2 GarageOffset { get => garageOffset; }

        public virtual string GetDetailCharacteristics()
        {
            return string.Empty;
        }
    }
}

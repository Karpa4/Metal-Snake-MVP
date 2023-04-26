public static class ConstantVariables
{
    // Game Setting
    public const float BULLET_TIME_TO_DESTROY = 5;
    public const float BulletSizeModifier = 1.5f; // на сколько увеличивается пуля после появления
    public const float BulletSizeDuration = 0.5f; // за какое время увеличивается пуля после появления
    public const float MinSizeForShotgunBullet = 0.4f; // минимальное значение для стартового размера пули дробовика
    public const float MaxSizeForShotgunBullet = 0.9f;// максимальное значение для стартового размера пули дробовика
    public const float CollisionHealthModifier = 3;
    public const float MinCollisionPowerForSound = 2;
    public const float DelayBeetwenRepeatCollision = 0.2f;
    public const int StartPremiumMoneyCount = 500;
    public const int StartMoneyCount = 0;
    public const int RewardMoneyPerCarriage = 100;
    public const int RewardExpPerCarriage = 100;
    public const float TimeBetweenDeathAndActiveLoseScreen = 1.5f;
    public const float MinSwipeLenghtForChangeDirection = 5;

    // Scene Name
    public const string MainMenuSceneName = "MainMenu";
    public const string GameSceneName = "MvpLevel";

    // File Path
    public const string UIRootPath = "UI/UIRoot";
    public const string PlayerPrefabPath = "Game/Player";
    public const string PlayerCarDataPath = "Game/PlayerCarData";
    public const string SoundsPath = "Audio/SoundStaticData";
    public const string AllDetailsPath = "Game/AllDetails";
    public const string UIPlayerPrefabPath = "Game/UIPlayer";
    public const string WindowsStaticDataPath = "StaticData/Windows/WindowsStaticData";
    public const string CoinsSprite = "UI/Coins";
    public const string MicrochipSprite = "UI/Chip";
    public const string AudioMixer = "Audio/MainMixer";

    // Tag
    public const string ObstacleTag = "Obstacle";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_OBSTACLE = "Obstacle";
    public const string TAG_PROJECTILE = "Projectile";
    public const string TAG_CARRIAGE = "Carriage";
    public const string TAG_ENEMY_TARGET_COLLIDER = "EnemyTargetCollider";

    // PlayerPrefs Key
    public const string MoneyKey = "Money";
    public const string PremiumMoneyKey = "PremiumMoney";
    public const string LevelKey = "Level";
    public const string CurrentExpKey = "CurExp";
    public const string EffectsVolume = "EffectsVolume";
    public const string MusicVolume = "MusicVolume";
    public const string IncreasedVolume = "IncreasedVolume";
    public const string EffectsVolumeSlider = "EffectsVolumeSlider";
    public const string MusicVolumeSlider = "MusicVolumeSlider";
}

using UnityEngine;

public static class CurrencyImages
{
    public static Sprite CoinsSprite { get; private set; }
    public static Sprite MicrochipSprite { get; private set; }

    public static void Load()
    {
        CoinsSprite = Resources.Load<Sprite>(ConstantVariables.CoinsSprite);
        MicrochipSprite = Resources.Load<Sprite>(ConstantVariables.MicrochipSprite);
    }
}

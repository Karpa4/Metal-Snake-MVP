using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSpriteController : MonoBehaviour
{
    [SerializeField] private GameObject aimSprite;

    public void EnableAimSprite()
    {
        aimSprite.SetActive(true);
    }

    public void DisableAimSprite()
    {
        aimSprite.SetActive(false);
    }
}

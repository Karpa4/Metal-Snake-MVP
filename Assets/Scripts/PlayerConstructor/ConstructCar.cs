using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Game.PlayerConstructor
{
    public class ConstructCar : MonoBehaviour
    {
        [SerializeField] private MainPlayer mainPlayer;
        [SerializeField] private CarDriver carDriver;
        [SerializeField] private CarPause carPause;
        [SerializeField] private BaseRotateWeapon playerWeapon;
        [SerializeField] private PlayerColliderData bodyColliderData;
        [SerializeField] private CarriageManager carriageManager;
        [SerializeField] private AdditionalWeapon additionalWeapon;

        [Header("Transforms")]
        [SerializeField] private Transform mainWeaponTransform;
        [SerializeField] private Transform bumperTransform;

        [Header("Sprite Renders")]
        [SerializeField] private SpriteRenderer bodyRender;
        [SerializeField] private List<SpriteRenderer> wheelsRenders;
        [SerializeField] private SpriteRenderer engineRender;
        [SerializeField] private SpriteRenderer secondWeaponRender;
        [SerializeField] private SpriteRenderer GraffityRender;
        [SerializeField] private SpriteRenderer secondStandRender;
        [SerializeField] private SpriteRenderer platformRender;
        [SerializeField] private SpriteRenderer ChangeBodyColorRender;

        [Header("For tests")]
        [SerializeField] private Body testBody;
        [SerializeField] private Bumper testBumper;
        [SerializeField] private Engine testEngine;
        [SerializeField] private Wheels testWheels;
        [SerializeField] private MainWeapon testMainWeapon;
        [SerializeField] private SecondWeapon testSecondWeapon;
        [SerializeField] private Detail testGraffity;
        [SerializeField] private Color testColor;

        public event Action<BaseRotateWeapon> MainWeaponChanged;

        private bool wasConstruct = false;
        private Body currBody;
        private Bumper currBumper;
        private Engine currEngine;
        private Wheels currWheels;
        private MainWeapon currMain;
        private SecondWeapon currSecond;

        private PlayerColliderData bumperColliderData = null;
        private GameObject mainWeaponGO = null;

        private void Start()
        {
            MainWeaponChanged += mainPlayer.SetMainWeapon;
            MainWeaponChanged += additionalWeapon.SetMainWeapon;
            MainWeaponChanged += carPause.SetMainWeapon;
            StartCoroutine(StartTestConstruct());
        }

        public void Construct(CarData carData)
        {
            carDriver.Construct(carData);
            mainPlayer.PlayerHealth.Init(carData.MaxHP);
        }

        public void NewConstruct(List<Detail> details)
        {
            wasConstruct = true;
            currBody = (Body)details[0];
            currBumper = (Bumper)details[1];
            currEngine = (Engine)details[2];
            currWheels = (Wheels)details[3];
            currMain = (MainWeapon)details[4];
            currSecond = (SecondWeapon)details[5];
            if (details.Count == 7)
            {
                SetGraffitiSprite(details[6].GameDetailSprite);
            }
            ChangeCarDetails();
        }

        public void SetColor(Color newColor)
        {
            ChangeBodyColorRender.color = newColor;
        }

        private void TestConstruct()
        {
            currBody = testBody;
            currBumper = testBumper;
            currEngine = testEngine;
            currWheels = testWheels;
            currMain = testMainWeapon;
            currSecond = testSecondWeapon;
            ChangeCarDetails();
            SetGraffitiSprite(testGraffity.GameDetailSprite);
            SetColor(testColor);
        }

        private void ChangeCarDetails()
        {
            ConstructBumper();
            SetMainWeapon();
            ConstructBody();
            ConstructEngine();
            ConstructSecondWeapon();
            ConstructWheels();
        }

        private void ConstructSecondWeapon()
        {
            secondWeaponRender.sprite = currSecond.GameDetailSprite;
            secondStandRender.sprite = currSecond.SecondStandSprite;
            additionalWeapon.SetWeaponParams(currSecond.WeaponStats, currSecond.Bullets);
        }

        private void ConstructEngine()
        {
            engineRender.sprite = currEngine.GameDetailSprite;
            carriageManager.ChangeSlowModifier(currEngine.CarriageSlowModifyer);
            carDriver.SetAccelerationWithModifier(currEngine.AccelerationModifyer);
        }

        private void ConstructBody()
        {
            bodyColliderData.ConstructHealth(mainPlayer.PlayerHealth);
            bodyRender.sprite = currBody.GameDetailSprite;
            platformRender.sprite = currBody.PlatformSprite;
        }

        private void ConstructBumper()
        {
            if (bumperColliderData != null)
            {
                Destroy(bumperColliderData.gameObject);
            }
            bumperColliderData = Instantiate(currBumper.BumperColliderGO, bumperTransform.position, Quaternion.identity, bumperTransform);
            bumperColliderData.ConstructHealth(mainPlayer.PlayerHealth);
            bumperColliderData.ConstructModifiers(currBumper.CollisionDamageToPlayerModifier, currBumper.CollisionDamageToEnemyModifier);
            mainPlayer.SetBumperModifier(currBumper.DamageFromObstacleModifier);
        }

        private void ConstructWheels()
        {
            carDriver.SetTurnAccelerationWithModifier(currWheels.TurnSpeedModifier);
            bodyColliderData.ConstructModifiers(currWheels.CollisionDamageToPlayerModifier, currWheels.CollisionDamageToEnemyModifier);
            int wheelsCount = wheelsRenders.Count;
            for (int i = 0; i < wheelsCount; i++)
            {
                wheelsRenders[i].sprite = currWheels.GameDetailSprite;
            }
        }

        private void SetMainWeapon()
        {
            WeaponConstructHelper constructHelper = Instantiate(currMain.MainWeaponGO, mainWeaponTransform);
            BaseRotateWeapon newMainWeapon = constructHelper.GetComponent<BaseRotateWeapon>();
            MainWeaponChanged?.Invoke(newMainWeapon);

            secondStandRender.transform.position = constructHelper.SecondStandTransform.position;
            secondWeaponRender.transform.position = constructHelper.SecondWeaponTransform.position;

            if (mainWeaponGO != null)
            {
                Destroy(mainWeaponGO);
            }

            mainWeaponGO = constructHelper.gameObject;
            constructHelper.DestroyUseless();
        }

        private void SetGraffitiSprite(Sprite graffitiSprite)
        {
            GraffityRender.sprite = graffitiSprite;
        }

        private IEnumerator StartTestConstruct()
        {
            yield return new WaitForSeconds(0.1f);
            if (!wasConstruct)
            {
                TestConstruct();
            }
        }

        public void ChangeDetailInMainMenu(Detail detail, DetailType type)
        {
            switch (type)
            {
                case DetailType.Body:
                    currBody = (Body)detail;
                    ConstructBody();
                    break;
                case DetailType.Bumper:
                    currBumper = (Bumper)detail;
                    ConstructBumper();
                    break;
                case DetailType.Engine:
                    currEngine = (Engine)detail;
                    ConstructEngine();
                    break;
                case DetailType.Wheels:
                    currWheels = (Wheels)detail;
                    ConstructWheels();
                    break;
                case DetailType.MainWeapon:
                    currMain = (MainWeapon)detail;
                    SetMainWeapon();
                    break;
                case DetailType.SecondWeapon:
                    currSecond = (SecondWeapon)detail;
                    ConstructSecondWeapon();
                    break;
                case DetailType.Graffiti:
                    SetGraffitiSprite(detail.GameDetailSprite);
                    break;
            }
        }
    }
}

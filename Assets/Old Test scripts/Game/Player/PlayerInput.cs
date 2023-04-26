using UnityEngine;
using UnityEngine.EventSystems;
//using UI.HUD;

namespace Game.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
       // [SerializeField] private ArrowController arrowController;
        private Vector2 pointTouched;
        private Vector2 pointDirection;
        private Vector2 basicDirection;
        private bool correctTouch;
        private Vector2 screenCenter;
        private float newDirection;

        private void Awake()
        {
            screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            pointDirection = Vector2.up;
            basicDirection = Vector2.up;
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                pointTouched = touch.position;

                if (Input.GetTouch(0).phase == TouchPhase.Began && Vector2.Distance(pointTouched, screenCenter) > 150f 
                    && !EventSystem.current.IsPointerOverGameObject(0))
                {
                    correctTouch = true;
                   // arrowController.ActiveArrow();
                }

                if (correctTouch)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                       // arrowController.StartCoroutine(arrowController.DissolveArrow());
                        correctTouch = false;
                    }
                    else
                    {
                        pointDirection = pointTouched - screenCenter;
                        newDirection = Vector2.SignedAngle(basicDirection, pointDirection);
                        playerMovement.ChangeDirection(newDirection);
                       // arrowController.ChangeArrowPosAndRot(touch.position, Quaternion.Euler(0f, 0f, newDirection));
                    }
                }
            }
        }
    }
}

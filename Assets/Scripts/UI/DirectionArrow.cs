using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Windows.Game.HUD;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private ArrowController arrowController;
    private Controller controller;
    private GameObject mainPlayerTop;
    private Vector2 basicDirection;
    private bool isEnabled;
    //private Vector2 startPosition;

    private void Start()
    {
        mainPlayerTop = GameObject.FindGameObjectWithTag("Bumper");
        basicDirection = Vector2.up;
        isEnabled = false;
        //startPosition = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    public void SetController(Controller playerController)
    {
        controller = playerController;
        controller.onMove += MoveArrow;
        controller.onTouch += DisableArrow;
    }

    private void DisableArrow(bool isTouch)
    {
        if (isTouch == false)
        {
            isEnabled = false;
            arrowController.StartDissolve();
        }
    }

    private void MoveArrow(Vector2 newPosition)
    {
        if (!isEnabled)
        {
            isEnabled = true;
            arrowController.ActiveArrow();
        }
        ChangeArrowRot(newPosition);
        ChangeArrowPos(newPosition);
    }

    private void ChangeArrowPos(Vector2 newPosition) 
    {
        Vector3 mainPlayerTopPos = Camera.main.WorldToScreenPoint(mainPlayerTop.transform.position);
        mainPlayerTopPos.z = 0;
        transform.position = new Vector2(mainPlayerTopPos.x, mainPlayerTopPos.y) + newPosition;
        //transform.position = startPosition + newPosition;
    }

    private void ChangeArrowRot(Vector2 pointDirection)
    {
        float newAngleToRotate = Vector2.SignedAngle(basicDirection, pointDirection);
        transform.rotation = Quaternion.Euler(0, 0, newAngleToRotate);
    }
}

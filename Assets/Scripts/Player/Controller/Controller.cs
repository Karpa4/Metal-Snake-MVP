using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : IController
{
	public IController.OnMove onMove { get; set; }
	public IController.OnTouch onTouch { get; set; }
	public IController.OnSwipe onSwipe { get; set; }
	public IController.OnTouchPos onTouchPos { get; set; }

	private Vector2 startpos;
	private bool isTuched = false;
	private Vector2 total; // test
	private SimpleMouseControl simpleMouseControl;

	public void Init()
	{
		simpleMouseControl = new SimpleMouseControl();
		simpleMouseControl.Enable();
		EnableControll();
	}

	private void StartDrag(InputAction.CallbackContext contex)
	{
		startpos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		isTuched = true;
		onTouch?.Invoke(true);
		onTouchPos?.Invoke(startpos);
	}

	private void BeingDrag(InputAction.CallbackContext contex)
	{
		if (!isTuched)
		{
			return;
		}

		Vector2 currentPos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		total = (currentPos - startpos);
		if (total.magnitude >= ConstantVariables.MinSwipeLenghtForChangeDirection)
        {
			onMove?.Invoke(total);
		}
	}

	private void EndDrag(InputAction.CallbackContext contex) 
	{
		if (!isTuched)
		{
			return;
		}

		Vector2 currentPos = simpleMouseControl.Game.Position.ReadValue<Vector2>();
		total = (currentPos - startpos);
		if (total.magnitude >= ConstantVariables.MinSwipeLenghtForChangeDirection)
		{
			onMove?.Invoke(total);
		}

		isTuched = false;
		onTouch?.Invoke(false);
	}

	/// <summary>
	/// Если игрок коснулся врага то отключаем дальнейшую работу контроллера
	/// </summary>
	public void SetOffTouch()
    {
		isTuched = false;
	}

	/// <summary>
	/// Отключение контроллера при паузе
	/// </summary>
	public void DisableControll()
	{
		simpleMouseControl.Game.Down.performed -= StartDrag;
		simpleMouseControl.Game.Position.performed -= BeingDrag;
		simpleMouseControl.Game.Down.canceled -= EndDrag;
	}

	/// <summary>
	/// Активация контроллера в начале игры или после паузы
	/// </summary>
	public void EnableControll()
	{
		simpleMouseControl.Game.Down.performed += StartDrag;
		simpleMouseControl.Game.Position.performed += BeingDrag;
		simpleMouseControl.Game.Down.canceled += EndDrag;
	}

	public bool GetIsTouched()
    {
		return isTuched;
    }

	public Vector2 GetStartPos()
    {
		return startpos;
    }
}

using UnityEngine;
public interface IController
{
	delegate void OnMove(Vector2 difDir);
	OnMove onMove { get; set; }
	delegate void OnTouch(bool isTouched);
	OnTouch onTouch { get; set; }
	delegate void OnTouchPos(Vector2 pos);
	OnTouchPos onTouchPos { get; set; }
	delegate void OnSwipe(Vector2 axis);
	OnSwipe onSwipe { get; set; }
}

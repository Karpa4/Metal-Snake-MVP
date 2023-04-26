using UnityEngine;

[CreateAssetMenu(menuName = "GameData/CarData")]
public class CarData : ScriptableObject
{
	[SerializeField] private float speedMax;
	[SerializeField] private float speedMin;
	[SerializeField] private float acceleration;
	[SerializeField] private float brakeSpeed;
	[SerializeField] private float reverseSpeed;
	[SerializeField] private float idleSlowdown;

	[SerializeField] private float turnSpeedMax;
	[SerializeField] private float turnSpeedAcceleration;
	[SerializeField] private float turnIdleSlowdown;

	[SerializeField] private int maxHP;

	[SerializeField] private Sprite carSprite;
	[SerializeField] private string carName;


	public float SpeedMax { get => speedMax; }
	public float SpeedMin { get => speedMin; }
	public float Acceleration { get => acceleration; }
	public float BrakeSpeed { get => brakeSpeed; }
	public float ReverseSpeed { get => reverseSpeed; }
	public float IdleSlowdown { get => idleSlowdown; }
	public float TurnSpeedMax { get => turnSpeedMax; }
	public float TurnSpeedAcceleration { get => turnSpeedAcceleration; }
	public float TurnIdleSlowdown { get => turnIdleSlowdown; }
	public Sprite CarSprite { get => carSprite; }
	public int MaxHP { get => maxHP; }
	public string CarName { get => carName; }
}
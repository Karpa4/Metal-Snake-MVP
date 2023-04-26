using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
	public CarData carData; 
	[SerializeField] private Rigidbody2D carRigidbody;
	[SerializeField] private float speed; //
	private float turnSpeed;
	private float forwardAmount;
	private float turnAmount;
	private float maxSpeed;
	private float acceleration = 0;
	private float turnSpeedAcceleration = 0;

	public void Construct(CarData carData)
    {
		this.carData = carData;
    }

    private void Awake()
    {
		speed = 0;
		maxSpeed = carData.SpeedMax;
		turnSpeedAcceleration = carData.TurnSpeedAcceleration;
		acceleration = carData.Acceleration;
	}

    private void FixedUpdate()
	{
		if (forwardAmount > 0)
		{
			// Accelerating
			if (speed < maxSpeed)
			{
				speed += forwardAmount * acceleration * Time.fixedDeltaTime * (0.2f + maxSpeed - speed);
				if (speed > maxSpeed)
                {
					speed = maxSpeed;
				}
			}
			else if (speed > maxSpeed)
            {
				speed -= forwardAmount * acceleration * Time.fixedDeltaTime * (0.2f - maxSpeed + speed);
			}
		}
		if (forwardAmount < 0)
		{
			if (speed > 0)
			{
				// Braking
				speed += forwardAmount * carData.BrakeSpeed * Time.fixedDeltaTime;
			}
			else
			{
				// Reversing
				speed += forwardAmount * carData.ReverseSpeed * Time.fixedDeltaTime;
				speed = Mathf.Clamp(speed, carData.SpeedMin, maxSpeed);
			}
		}

		if (forwardAmount == 0)
		{
			// Not accelerating or braking
			if (speed > 0)
			{
				speed -= carData.IdleSlowdown * Time.fixedDeltaTime;
			}
			if (speed < 0)
			{
				speed += carData.IdleSlowdown * Time.fixedDeltaTime;
			}
		}
		carRigidbody.velocity = transform.up * speed;

		if (speed < 0)
		{
			// Going backwards, invert wheels
			turnAmount = turnAmount * -1f;
		}

		if (turnAmount > 0 || turnAmount < 0)
		{
			// Turning
			if ((turnSpeed > 0 && turnAmount < 0) || (turnSpeed < 0 && turnAmount > 0))
			{
				// Changing turn direction
				float minTurnAmount = 20;
				turnSpeed = turnAmount * minTurnAmount;
			}
			turnSpeed += turnAmount * turnSpeedAcceleration * Time.fixedDeltaTime;
		}
		else
		{
			// Not turning
			if (turnSpeed > 0)
			{
				turnSpeed -= carData.TurnIdleSlowdown * Time.fixedDeltaTime;
			}
			if (turnSpeed < 0)
			{
				turnSpeed += carData.TurnIdleSlowdown * Time.fixedDeltaTime;
			}
			if (turnSpeed > -1f && turnSpeed < +1f)
			{
				// Stop rotating
				turnSpeed = 0f;
			}
		}

		//float speedNormalized = speed / carData.SpeedMax;
		float speedNormalized = speed / maxSpeed;
		float invertSpeedNormalized = Mathf.Clamp(1 - speedNormalized, .75f, 1f);

		turnSpeed = Mathf.Clamp(turnSpeed, -carData.TurnSpeedMax, carData.TurnSpeedMax);

		carRigidbody.angularVelocity = turnSpeed * (invertSpeedNormalized * 1f) * (speed / 5f);
	}

	public void SetAccelerationWithModifier(float accelerationModifier)
    {
		acceleration = carData.Acceleration * accelerationModifier;
	}

	public void SetMaxSpeed(float newMaxSpeed)
    {
		maxSpeed = newMaxSpeed;
	}

	public void SetTurnAccelerationWithModifier(float turnModifier)
    {
		turnSpeedAcceleration = carData.TurnSpeedAcceleration * turnModifier;
    }

	public void SetInputs(float forwardAmount, float turnAmount)
	{
		this.forwardAmount = forwardAmount;
		this.turnAmount = turnAmount;
	}

	public int GetMaxHP()
    {
		return carData.MaxHP;
    }

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

	public float GetSpeed()
	{
		return speed;
	}

	public float GetMaxSpeed()
    {
		return maxSpeed;
    }

	public void SetMaxSpeed()
    {
		this.speed = maxSpeed;
    }
}

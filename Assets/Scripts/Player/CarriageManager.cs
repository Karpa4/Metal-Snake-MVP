using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Game.Player;

public class CarriageManager : MonoBehaviour
{
	[SerializeField] private MainPlayer mainPlayer;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private Carriage carriagePrefab;
	[SerializeField] private Transform targetForCarriage;
	
	private ExplosionController explosionScript;
	private List<Carriage> carriages = new List<Carriage>();
	private float slowPlayerModifier = 0.15f;
	public Action<int> OnCarriageChange;
	
    private void Start()
    {
		mainPlayer.PlayerHealth.onDeath += RemoveCarriage;

		explosionScript = FindObjectOfType<ExplosionController>();
	}

	public void ChangeSlowModifier(float slowModifier)
    {
		slowPlayerModifier *= slowModifier;
	}

	public void SwitchCarriages(bool isActive)
    {
		foreach (Carriage carriage in carriages)
        {
			carriage.enabled = isActive;
			carriage.SwitchDust(isActive);
        }
    }

    public void AddCarriage(Sprite carriageSprite)
	{
		int carriagesCount = carriages.Count;
		Debug.Log("AddCarriage");
		Transform target;
		if (carriagesCount == 0)
		{
			target = targetForCarriage;
		}
		else
		{
			target = carriages.Last().targetForCarriage;
		}

		Carriage newCarriage = Instantiate(carriagePrefab, target.position, target.rotation);
		newCarriage.Init(target, mainPlayer.PlayerHealth, carriageSprite);
		carriages.Add(newCarriage);
		ChangeCarriageCountEvent(carriagesCount + 1);
		ChangeMaxSpeed(carriagesCount + 1);
	}

	public int GetCarriagesCount()
	{
		return carriages.Count;
	}

	private void RemoveCarriage()
	{
		int carriagesCount = carriages.Count;
		if (carriagesCount == 0)
		{
			Debug.Log("GameOver");
			mainPlayer.Die();
			return;
		}
		var lastCarriage = carriages.Last();
		explosionScript.HeavyExplosion(lastCarriage.transform.position); // Взрыв прицепа
		Destroy(lastCarriage.gameObject);
		mainPlayer.PlayLightExplosionSound();
		carriages.RemoveAt(carriagesCount - 1);
		ChangeMaxSpeed(carriagesCount - 1);
		ChangeCarriageCountEvent(carriagesCount - 1);
		mainPlayer.PlayerHealth.FullRestore();
	}

	private void ChangeMaxSpeed(int carriagesCount)
    {
		float newSpeed = 0;
		if (carriagesCount > 2)
        {
			newSpeed = carDriver.carData.SpeedMax * Mathf.Pow((1 - slowPlayerModifier), carriagesCount - 2);
		}
		else
        {
			newSpeed = carDriver.carData.SpeedMax;

		}
		carDriver.SetMaxSpeed(newSpeed);
	}

	private void ChangeCarriageCountEvent(int newCarriageCount)
    {
        OnCarriageChange?.Invoke(newCarriageCount);
    }
}

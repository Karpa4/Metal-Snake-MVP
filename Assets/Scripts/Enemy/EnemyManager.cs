using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] private List<CarDriverAI> carDriversAI;
	[SerializeField] private Transform target;

    private void Awake()
    {
		foreach (CarDriverAI car in carDriversAI)
		{
			car.SetTargetTransform(target);
		}
	}
}

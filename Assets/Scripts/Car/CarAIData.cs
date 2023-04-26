using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/CarAIData")]
public class CarAIData : ScriptableObject
{
	[SerializeField] private float stoppingDistance = 30f;
	[SerializeField] private float stoppingSpeed = 40f;
	[SerializeField] private float reverseDistance = 25f;
	[SerializeField] private float reachedTargetDistance = 0;
	[SerializeField] private float viewDistance = 5f;

	public float StoppingDistance { get => stoppingDistance; }
	public float StoppingSpeed { get => stoppingSpeed; }
	public float ReverseDistance { get => reverseDistance; }
	public float ReachedTargetDistance { get => reachedTargetDistance; }
	public float ViewDistance { get => viewDistance; }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance { get => instance; set => instance = value; }

	protected virtual void Awake()
	{
		Instance = this as T;
	}
}
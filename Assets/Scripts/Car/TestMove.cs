using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
public class TestMove : MonoBehaviour
{
	[SerializeField] private Seeker seeker;
	private float lastRepath = float.NegativeInfinity;
	private Path path;
	private bool reachedEndOfPath;
	private int currentWaypoint = 0;
	[SerializeField] private float RepathRate;
	[SerializeField] private float nextWaypointDistance;
	Transform target;
	public void SetTransform(Transform tr)
	{
		target = tr;
		seeker.drawGizmos = true;
	}
	private void OnPathComplete(Path p)
	{
		//Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

		// Path pooling. To avoid unnecessary allocations paths are reference counted.
		// Calling Claim will increase the reference count by 1 and Release will reduce
		// it by one, when it reaches zero the path will be pooled and then it may be used
		// by other scripts. The ABPath.Construct and Seeker.StartPath methods will
		// take a path from the pool if possible. See also the documentation page about path pooling.
		p.Claim(this);
		if (!p.error)
		{
			if (path != null) path.Release(this);
			path = p;
			// Reset the waypoint counter so that we start to move towards the first point in the path
			currentWaypoint = 0;
		}
		else
		{
			p.Release(this);
		}

	}
	private void Update()
	{
		// Transform transform = transform;
		if (Time.time > lastRepath + RepathRate && seeker.IsDone())
		{
			lastRepath = Time.time;

			seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
		}

		if (path == null)
		{
			return;
		}
		reachedEndOfPath = false;
		float distanceToWaypoint;
		while (true)
		{
			distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
			if (distanceToWaypoint < nextWaypointDistance)
			{
				if (currentWaypoint + 1 < path.vectorPath.Count)
				{
					currentWaypoint++;
				}
				else
				{
					reachedEndOfPath = true;
					break;
				}
			}
			else
			{
				break;
			}
		}
		// var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / enemy.dataMovement.NextWaypointDistance) : 1f;

	}
}

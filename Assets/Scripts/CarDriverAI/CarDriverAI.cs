using System.Linq;
using Pathfinding;
using UnityEngine;
using System;

public class CarDriverAI : MonoBehaviour
{
	[SerializeField] private CarAIData carAIData;
	[SerializeField] private CarDriver carDriver;
	[SerializeField] private LayerMask obstaclesMask;
	[SerializeField] private Seeker seeker;

	public event Action InitEnemy;

	private Transform targetPositionTranformMain;
	private Vector2 targetPosition;
	private Path path;
	private Vector2? avoidPoint;
	private float offsetCastBehindObstacle = 3;

	private void Update()
	{
		var v = AvoidObstaclePoint();
		if (v != null)
			avoidPoint = v;
		ChosePointToGo();
		DriveToTarget();
	}

	private void DriveToTarget()
	{
		float forwardAmount = 0f;
		float turnAmount = 0f;

		float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
		if (distanceToTarget > carAIData.ReachedTargetDistance)
		{
			// Still too far, keep going
			Vector2 dirToMovePosition = ((Vector3)targetPosition - transform.position).normalized;
			float dot = Vector2.Dot(transform.up, dirToMovePosition);

			if (dot > 0)
			{
				// Target in front
				forwardAmount = 1f;


				if (distanceToTarget < carAIData.StoppingDistance && carDriver.GetSpeed() > carAIData.StoppingSpeed)
				{
					// Within stopping distance and moving forward too fast
					forwardAmount = -1f;
				}
			}
			else
			{
				// Target behind
				if (distanceToTarget > carAIData.ReverseDistance)
				{
					// Too far to reverse
					forwardAmount = 1f;
				}
				else
				{
					forwardAmount = -1f;
				}
			}

			float angleToDir = Vector2.SignedAngle(transform.up, dirToMovePosition);
			if (angleToDir > 0)
			{
				turnAmount = 1f;
			}
			else
			{
				turnAmount = -1f;
			}
		}
		else
		{
			// Reached target
			if (carDriver.GetSpeed() > 15f)
			{
				forwardAmount = -1f;
			}
			else
			{
				forwardAmount = 0f;
			}
			turnAmount = 0f;
		}
		carDriver.SetInputs(forwardAmount, turnAmount);
	}

	private void ChosePointToGo()
	{
		if (targetPositionTranformMain != null &&
		(avoidPoint == null || Vector2.Dot(transform.up, (Vector2)avoidPoint - (Vector2)transform.position) < 0))//Behind
		{
			if (avoidPoint != null && Vector2.Dot(transform.up, (Vector2)avoidPoint - (Vector2)transform.position) < 0)
				avoidPoint = null;
			SetTargetPosition(targetPositionTranformMain.position);
		}
		else if (avoidPoint != null)
			SetTargetPosition((Vector2)avoidPoint);
		else
			SetTargetPosition(transform.position + transform.up * carAIData.ViewDistance);
	}

	public void SetTargetPosition(Vector2 targetPosition)
	{
		this.targetPosition = targetPosition;
	}

	public Transform GetPlayerTransform()
    {
		return targetPositionTranformMain;
	}

	public void SetTargetTransform(Transform playerTransform)
	{
		targetPositionTranformMain = playerTransform;
		InitEnemy?.Invoke();
	}
	Vector3? AvoidObstaclePoint()
	{
		Vector3? avoidPoint = null;
		RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position, Vector2.one, 0, (Vector2)transform.up, carAIData.ViewDistance, obstaclesMask);
		if (hit.collider == null)
			return null;

		GraphNode node = AstarPath.active.GetNearest(hit.point).node;
		if (node.Walkable)
			return null;

		GraphNode nearestWalkableNodeB = AstarPath.active.GetNearest(((Vector3)hit.point) + transform.up * offsetCastBehindObstacle, NNConstraint.Default).node;
		if (nearestWalkableNodeB == null)
			return null;

		seeker.StartPath((Vector3)transform.position, (Vector3)nearestWalkableNodeB.position, OnPathComplete);
		Vector3 p1 = transform.position;
		Vector3 p2 = transform.position + transform.up;

		if (path == null)
			return null;
		var pathPoints = path.vectorPath.OrderByDescending(x => GetDistanceFromPointToLine(p1.x, p1.y, p2.x, p2.y, x.x, x.y)).ToList();
		if (pathPoints == null || pathPoints.Count == 0)
			return null;
		Vector3 point = pathPoints[0];
		avoidPoint = point;
		return avoidPoint;

	}
	private void OnPathComplete(Path p)
	{
		p.Claim(this);
		if (!p.error)
		{
			if (path != null) path.Release(this);
			path = p;
		}
		else
		{
			p.Release(this);
		}
	}
	float GetDistanceFromPointToLine(float ax, float ay, float bx, float by, float x, float y)
	{
		float a, b, c;
		a = ay - by;
		b = bx - ax;
		c = ax * by - bx * ay;
		return Mathf.Abs(a * x + b * y + c) / Mathf.Sqrt(a * a + b * b);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TargetsManager 
{
	private static List<ObjectToHit> allTargets;

	static TargetsManager()
	{
		allTargets = new List<ObjectToHit>();
	}

	public static bool AddTarget(ObjectToHit newTarget)
	{
		if (!allTargets.Contains(newTarget))
		{
			allTargets.Add(newTarget);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool RemoveTarget(ObjectToHit target)
	{
		if (allTargets.Contains(target))
		{
			allTargets.Remove(target);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static List<ObjectToHit> FindTargetsInRadius(ObjectToHit head, float radius)
	{
		List<ObjectToHit> targetsInRadius = new List<ObjectToHit>();
		foreach (ObjectToHit target in allTargets)
		{
			if (target != head)
			{
				if (Vector3.Distance(head.center.position, target.center.position) <= radius)
				{
					targetsInRadius.Add(target);
				}
			}
		}

		return targetsInRadius;
	}

	public static List<ObjectToHit> FindTargetsInRadius(Vector3 viewer, float radius)
	{
		List<ObjectToHit> targetsInRadius = new List<ObjectToHit>();
		foreach (ObjectToHit target in allTargets)
		{
			if (Vector3.Distance(viewer, target.center.position) <= radius)
			{
				targetsInRadius.Add(target);
			}
		}

		return targetsInRadius;
	}

	public static ObjectToHit GetNearestTarget(ObjectToHit head, float radius)
	{
		ObjectToHit nearestTarget = null;
		float minDistance = 1000;

		foreach (ObjectToHit target in allTargets)
		{
			if (target != head)
			{
				float distance = Vector3.Distance(head.center.position, target.center.position);
				if (distance <= radius && distance < minDistance)
				{
					nearestTarget = target;
				}
			}
		}

		return nearestTarget;
	}


}

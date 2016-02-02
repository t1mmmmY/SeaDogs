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

	public static List<ObjectToHit> FindTargetsInRadius(Vector3 position, float radius)
	{
		List<ObjectToHit> targetsInRadius = new List<ObjectToHit>();
		foreach (ObjectToHit target in allTargets)
		{
			if (Vector3.Distance(position, target.center.position) <= radius)
			{
				targetsInRadius.Add(target);
			}
		}

		return targetsInRadius;
	}

}

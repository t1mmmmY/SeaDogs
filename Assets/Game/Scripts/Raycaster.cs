using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Raycaster 
{
	public static ObjectToHit Raycast(Transform viewer)
	{
		RaycastHit hit;
		if (Physics.Raycast(viewer.position, viewer.forward, out hit, 10, 1 << 8))
		{
			ObjectToHit objectToHit = hit.collider.GetComponent<ObjectToHit>();

			if (objectToHit != null)
			{
				return objectToHit;
			}
		}

		return null;

//		RaycastHit[] allHits = Physics.BoxCastAll(transform.position, new Vector3(1, 1, 5), transform.forward, transform.rotation, 10, 1 << 8);
//		List<ObjectToHit> allObjectsToHit = new List<ObjectToHit>();
//
//		foreach (RaycastHit hit in allHits)
//		{
//			ObjectToHit objectToHit = hit.collider.GetComponent<ObjectToHit>();
//			if (objectToHit != null && hit.collider.gameObject != this.gameObject)
//			{
//				allObjectsToHit.Add(objectToHit);
//				Debug.Log(objectToHit.name);
//			}
//		}

	}


	public static ObjectToHit FindClosestTarget(Transform viewer, float radius, float fieldOfViewRange, ObjectToHit viewerObject) 
	{
		List<ObjectToHit> allTargets = TargetsManager.FindTargetsInRadius(viewer.position, radius);
		Dictionary<ObjectToHit, Vector2> possibleTargets = new Dictionary<ObjectToHit, Vector2>(); //object, Vector2(distance, angle)

		foreach (ObjectToHit target in allTargets)
		{
//			RaycastHit hit; 
			Vector3 rayDirection = target.transform.position - viewer.position; //???
			float angle = Vector3.Angle(rayDirection, viewer.forward);

			if (angle < fieldOfViewRange)
			{
				RaycastHit[] hits = Physics.RaycastAll(viewer.position, rayDirection);
//				if (Physics.RaycastAll(viewer.position, rayDirection, out hit)) 
				foreach (RaycastHit hit in hits)
				{
					ObjectToHit obj = hit.transform.GetComponent<ObjectToHit>();
					if (obj == target && obj != viewerObject && !possibleTargets.ContainsKey(target))
					{
						//This is possible target
						possibleTargets.Add(target, new Vector2(Vector3.Distance(viewer.position, target.center.position), angle));
					}
					else
					{
						//Target behind the obstancle
//						return false;
					}
				}
			}
		}

		return GetBestMatch(possibleTargets);

	}

	public static ObjectToHit GetBestMatch(Dictionary<ObjectToHit, Vector2> possibleTargets) 
	{
		float minAngle = 100;
		ObjectToHit bestMatch = null;

		foreach (ObjectToHit target in possibleTargets.Keys)
		{
			if (possibleTargets[target].y < minAngle)
			{
				minAngle = possibleTargets[target].y;
				bestMatch = target;
			}
		}

		return bestMatch;
	}

}

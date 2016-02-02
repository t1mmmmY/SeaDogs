using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour 
{
	[SerializeField] ThirdPersonCharacter movementController;
	[SerializeField] SwordAIControl swordController;
	[SerializeField] ObjectToHit thisObjectToHit;
	[SerializeField] float visibleRadius = 10.0f;

	[SerializeField] float distanceToHit = 1.5f;

	[SerializeField] ObjectToHit target;

	void Awake()
	{
		if (movementController == null)
		{
			movementController = GetComponent<ThirdPersonCharacter>();
		}
		if (swordController == null)
		{
			swordController = GetComponent<SwordAIControl>();
		}
		if (thisObjectToHit == null)
		{
			thisObjectToHit = GetComponent<ObjectToHit>();
		}
	}

	void Update()
	{
		if (target == null)
		{
			ObjectToHit nearestTarget = TargetsManager.GetNearestTarget(thisObjectToHit, visibleRadius);
			if (nearestTarget != null)
			{
				target = nearestTarget;
			}
		}

		if (target != null)
		{
			float distance = Vector3.Distance(transform.position, target.transform.position);
			if (distance > distanceToHit)
			{
				Vector3 direction = target.transform.position - transform.position;
				movementController.Move(direction, false, false);
			}
			else
			{
				movementController.Stop();
				swordController.Hit();
			}
		}
	}


}

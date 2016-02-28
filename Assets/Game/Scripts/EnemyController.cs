using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class EnemyController : MonoBehaviour 
{
	[SerializeField] ThirdPersonCharacter movementController;
	[SerializeField] SwordAIControl swordController;
	[SerializeField] ObjectToHit thisObjectToHit;
	[SerializeField] NavMeshAgent agent;           // the navmesh agent required for the path finding

//	[SerializeField] AICharacterControl characterControl;
	[SerializeField] float visibleRadius = 10.0f;

	[SerializeField] float velocityToHit = 1.0f;
	[SerializeField] float focusDistance = 4.0f;

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
		if (agent == null)
		{
			agent = GetComponent<NavMeshAgent>();
		}
	}


	void Update()
	{
		if (NeedToFindNewTarget())
		{
			ObjectToHit nearestTarget = TargetsManager.GetNearestTarget(thisObjectToHit, visibleRadius);

			target = nearestTarget;

//			characterControl.SetTarget(target.transform);
		}

//		if (target == null)
//		{
//			ObjectToHit nearestTarget = TargetsManager.GetNearestTarget(thisObjectToHit, visibleRadius);
//			if (nearestTarget != null)
//			{
//				target = nearestTarget;
//			}
//		}

		if (target != null)
		{
			agent.SetDestination(target.transform.position);

			if (agent.remainingDistance < focusDistance)
			{
				movementController.FocusOnTarget(target.transform);
			}
			else if (movementController.target != null)
			{
				movementController.Unfocus();
			}

//			float distance = Vector3.Distance(transform.position, target.transform.position);
			if (agent.remainingDistance > agent.stoppingDistance)
//			if (distance > distanceToHit)
			{
				Vector3 direction = target.transform.position - transform.position;
				movementController.Move(direction, false, true);
				swordController.Run();
				swordController.StopHit();
			}
			else if (agent.velocity.magnitude < velocityToHit)
			{
				movementController.Stop();
				swordController.Stop();
				swordController.Hit();
			}
		}
	}


	bool NeedToFindNewTarget()
	{
		if (target == null)
		{
			return true;
		}
		else 
		{
			return false;
		}
	}


}

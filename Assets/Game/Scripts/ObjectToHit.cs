using UnityEngine;
using System.Collections;

public class ObjectToHit : MonoBehaviour 
{
	[SerializeField] private Transform centerObject;

	void OnEnable()
	{
		TargetsManager.AddTarget(this);
	}

	void OnDisable()
	{
		TargetsManager.RemoveTarget(this);
	}


	public Transform center
	{
		get 
		{ 
			if (centerObject != null)
			{
				return centerObject; 
			}
			else
			{
				return transform;
			}
		}
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFocus : MonoBehaviour 
{
	[SerializeField] bool focusingAlways = true;
	[SerializeField] Transform viewer;
	bool focusing = false;
	ObjectToHit focusedObject;

//	ThirdPersonCharacter thirdPersonCharacter;
//
//	void Awake()
//	{
//		thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
//		if (thirdPersonCharacter == null)
//		{
//			Debug.LogError("thirdPersonCharacter == null");
//		}
//	}

	void Start()
	{
	}

	void Update()
	{

		if (focusingAlways)
		{
			focusedObject = Raycast();

			if (Input.GetKeyDown(KeyCode.F))
			{
				Focus();
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				//BeginFocus
				focusing = true;
			}
	//		if (Input.GetKey(KeyCode.F))
	//		{
	//		}
			if (Input.GetKeyUp(KeyCode.F))
			{
				focusing = false;
				Focus();
			}

			if (focusing)
			{
				Focusing();
			}
		}
	}

	void Focusing()
	{
		focusedObject = Raycast();
	}

	void Focus()
	{
		if (focusedObject != null)
		{
			Debug.Log("Focus on object " + focusedObject.name);
		}
	}

	void Unfocus()
	{
	}

	ObjectToHit Raycast()
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



		return null;
	}


}

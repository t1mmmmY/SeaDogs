using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour 
{
	[SerializeField] int characterID = 0;
	[SerializeField] bool isSword = false;

	public int ID
	{
		get { return characterID; }
	}

	public bool IsSword
	{
		get { return isSword; }
	}

	void OnTriggerEnter(Collider other)
	{
		BodyPart enemyPart = other.gameObject.GetComponent<BodyPart>();

		if (enemyPart != null)
		{
			if (enemyPart.ID != characterID && enemyPart.IsSword)
			{
				Debug.Log(this.gameObject.name + " hit by " + other.gameObject.name);
			}
		}
	}

//	void OnCollisionEnter(Collision collision) 
//	{
//		BodyPart enemyPart = collision.gameObject.GetComponent<BodyPart>();
//
//		if (enemyPart != null)
//		{
//			if (enemyPart.ID != characterID && enemyPart.IsSword)
//			{
//				Debug.Log(this.gameObject.name + " hit by " + collision.gameObject.name);
//			}
//		}
//	}
}

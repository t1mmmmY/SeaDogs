using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour 
{
	[SerializeField] int characterID = 0;
	[Range(1.0f, 100.0f)]
	[SerializeField] float healthValue = 10.0f;
	[SerializeField] bool isSword = false;

	CharacterStats stats;

	public int ID
	{
		get { return characterID; }
	}

	public bool IsSword
	{
		get { return isSword; }
	}

	void Awake()
	{
		stats = GetComponentInParent<CharacterStats>();

		if (stats == null)
		{
			Debug.LogError("Can not found CharacterStats! " + this.gameObject.name);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		BodyPart enemyPart = other.gameObject.GetComponent<BodyPart>();

		if (enemyPart != null)
		{
			if (enemyPart.ID != characterID && enemyPart.IsSword)
			{
				Sword enemySword = enemyPart.GetComponent<Sword>();

				if (enemySword.swordState == SwordState.Hit)
				{
					if (this.isSword)
					{
						//Sword hit sword
//						Debug.LogWarning("Blocked");
//						enemySword.BlockedBySword();
					}
					else
					{
						//body hitted by enemy sword
//						Debug.Log(this.gameObject.name + " hit by " + other.gameObject.name);
						//Need to calculate hit force also
						stats.Hit(healthValue, this);
					}
				}

			}
		}
	}

	void OnCollisionEnter(Collision collision) 
	{
		BodyPart enemyPart = collision.gameObject.GetComponent<BodyPart>();

		if (enemyPart != null)
		{
			if (enemyPart.ID != characterID && enemyPart.IsSword)
			{
				Sword enemySword = enemyPart.GetComponent<Sword>();

				if (enemySword.swordState == SwordState.Hit)
				{
					if (this.isSword)
					{
						//Sword hit sword
//						Debug.LogWarning("Blocked");
						enemySword.BlockedBySword();
					}
//					else
//					{
//						//body hitted by enemy sword
////						Debug.Log(this.gameObject.name + " hit by " + other.gameObject.name);
//						//Need to calculate hit force also
//						stats.Hit(healthValue, this);
//					}
				}

			}
		}
	}
}

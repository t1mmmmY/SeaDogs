using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour 
{
	[SerializeField] float _hp = 100;
	public float HP 
	{
		get { return _hp; }
		private set { _hp = value; }
	}

	public bool Hit(float hitValue, BodyPart bodyPart)
	{
		HP -= hitValue;

		HP = Mathf.Clamp(HP, 0.0f, 100.0f);

//		Debug.LogWarning(this.gameObject.name + " health left " + HP.ToString());

		if (HP == 0)
		{
			Dead();
			//Is alive? False
			return false;
		}
		else
		{
			//Is alive? True
			return true;
		}
	}

	void Dead()
	{
	}

}

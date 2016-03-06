using UnityEngine;
using System.Collections;

public enum SwordState
{
	Idle,
	Block,
	Swing,
	Hit,
}

public class Sword : MonoBehaviour 
{
	CapsuleCollider swordCollider;
	Rigidbody swordRigidbody;
	SwordControl swordControl;
	public SwordState swordState { get; private set; }

	void Awake()
	{
		swordCollider = this.GetComponent<CapsuleCollider>();
		swordRigidbody = this.GetComponent<Rigidbody>();
	}

	public void Init(SwordControl swordControl)
	{
		this.swordControl = swordControl;
		swordState = SwordState.Idle;
	}

	public void Swing()
	{
		swordState = SwordState.Swing;
	}

	public void Hit()
	{
		swordState = SwordState.Hit;
	}

	public void Block()
	{
		swordState = SwordState.Block;
	}

	public void Idle()
	{
		swordState = SwordState.Idle;
	}


	public void BlockedBySword()
	{
		swordState = SwordState.Idle;
		swordControl.BlockedBySword();
	}

}

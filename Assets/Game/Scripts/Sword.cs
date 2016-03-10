using UnityEngine;
using System.Collections;
using RootMotion.Demos;
//using RootMotion.FinalIK;

public enum SwordState
{
	Idle,
	Block,
	Swing,
	Hit,
}

public class Sword : MonoBehaviour 
{
	BoxCollider swordCollider;
	Rigidbody swordRigidbody;
	SwordControl swordControl;
	MotionAbsorb motionAbsorb;
	public SwordState swordState { get; private set; }

//	void Awake()
//	{
//		swordCollider = this.GetComponent<CapsuleCollider>();
//		swordRigidbody = this.GetComponent<Rigidbody>();
//
//		if (motionAbsorb == null)
//		{
//			motionAbsorb = this.GetComponent<MotionAbsorb>();
//		}
//	}

	public void Init(SwordControl swordControl)
	{
		swordCollider = this.GetComponent<BoxCollider>();
		swordRigidbody = this.GetComponent<Rigidbody>();

		motionAbsorb = this.GetComponent<MotionAbsorb>();
		this.swordControl = swordControl;

		motionAbsorb.weight = 0.0f;
		swordState = SwordState.Idle;
		swordCollider.enabled = false;
	}

	public void Swing()
	{
		motionAbsorb.weight = 0.0f;
		swordState = SwordState.Swing;
		swordCollider.enabled = false;
	}

	public void Hit()
	{
		motionAbsorb.weight = 1.0f;
		swordState = SwordState.Hit;
		swordCollider.enabled = true;
	}

	public void Block()
	{
		motionAbsorb.weight = 0.0f;
		swordState = SwordState.Block;
		swordCollider.enabled = true;
	}

	public void Idle()
	{
		motionAbsorb.weight = 0.0f;
		swordState = SwordState.Idle;
		swordCollider.enabled = false;
	}


	public void BlockedBySword()
	{
		swordControl.BlockedBySword();

		motionAbsorb.weight = 0.0f;
		swordState = SwordState.Idle;
		swordCollider.enabled = false;
	}

}

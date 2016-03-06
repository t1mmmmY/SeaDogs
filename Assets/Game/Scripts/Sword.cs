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
	public SwordState swordState { get; private set; }

	void Awake()
	{
		swordCollider = this.GetComponent<CapsuleCollider>();
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

}

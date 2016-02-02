using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordBaseControl : MonoBehaviour 
{
	protected enum AnimationState
	{
		Nothing,
		Swing,
		Hit,
		Block,
		FinishBlock
	}

	protected SwordControl swordControl;
	protected AnimationState state;
	protected bool isRunning = false;



	protected bool isSwing = false;
	protected bool isSwingHold = false;
	protected bool isHit = false;
	protected bool isBlock = false;
	protected bool isBlockHold = false;
	protected bool isBlockFinish = false;
	protected bool runNow = false;



	protected virtual void Awake()
	{
		swordControl = GetComponent<SwordControl>();
		state = AnimationState.Nothing;
	}


	protected virtual void Update()
	{
		bool changeRunningState = false;
		if (isRunning != runNow)
		{
			isRunning = runNow;
			changeRunningState = true;
		}

		switch (state)
		{
			case AnimationState.Nothing:
				//Can swing, block
				if (isSwing || isSwingHold)
				{
					//										DisableOldDirection();
					ChangeDirection();
					Swing();
				}
				else if (isBlock || isBlockHold)
				{
					//										DisableOldDirection();
					ChangeDirection();
					Block();
				}
				break;
			case AnimationState.Swing:
				//Can hit, block, change direction
				ChangeDirection();

				if (changeRunningState)
				{
					ChangeRunningState();
				}

				if (isBlock)
				{
					Block();
				}
				else if (isHit)
				{
					Hit();
				}
				break;
			case AnimationState.Hit:
				//wait for end hit. Can do nothing
				if (changeRunningState)
				{
					ChangeRunningState();
				}

				break;
			case AnimationState.Block:
				//Can finish block, swing, change direction
				ChangeDirection();

				if (changeRunningState)
				{
					ChangeRunningState();
				}

				if (isSwing)
				{
					Swing();
				}
				else if (isBlockFinish)
				{
					FinishBlock();
				}
				break;
			case AnimationState.FinishBlock:
				//I'm not sure that this is needed
				if (changeRunningState)
				{
					ChangeRunningState();
				}

				state = AnimationState.Nothing;
				break;
		}
	}



	protected virtual void ChangeRunningState()
	{
		if (isRunning)
		{
			swordControl.BeginRunning();
		}
		else
		{
			swordControl.StopRunning();
		}
	}


	protected virtual void Swing()
	{
		state = AnimationState.Swing;
		swordControl.Swing(isRunning);
		//		Debug.Log("Swing");
	}

	protected virtual void Hit()
	{
		state = AnimationState.Hit;
		swordControl.Hit(isRunning, OnFinishHit);
		//		Debug.Log("Hit");
	}

	protected virtual void Block()
	{
		state = AnimationState.Block;
		swordControl.Block(isRunning);
		//		Debug.Log("Block");
	}

	protected virtual void FinishBlock()
	{
		state = AnimationState.FinishBlock;
		swordControl.FinishBlock();
		//		Debug.Log("FinishBlock");
	}

	protected virtual void ChangeDirection()
	{
		//Maybe this not needed
	}

	protected virtual void OnFinishHit()
	{
		state = AnimationState.Nothing;
		//		Debug.Log("OnFinishHit");
	}

	protected virtual void OnFinishAnimation()
	{
	}

}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	enum AnimationState
	{
		Nothing,
		Swing,
		Hit,
		Block,
		FinishBlock
	}

	SwordControl swordControl;

	AnimationState state;
	bool isRunning = false;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
		state = AnimationState.Nothing;
	}


	void Update()
	{
		bool isSwing = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
		bool isSwingHold = (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E));
		bool isHit = Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E);
		bool isBlock = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space);
		bool isBlockHold = (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space));
		bool isBlockFinish = Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space);
		bool runNow = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

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
					ChangeDirection();
					Swing();
				}
				else if (isBlock || isBlockHold)
				{
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

	void ChangeRunningState()
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


	void Swing()
	{
		state = AnimationState.Swing;
		swordControl.Swing(isRunning);
		Debug.Log("Swing");
	}

	void Hit()
	{
		state = AnimationState.Hit;
		swordControl.Hit(isRunning, OnFinishHit);
		Debug.Log("Hit");
	}

	void Block()
	{
		state = AnimationState.Block;
		swordControl.Block(isRunning);
		Debug.Log("Block");
	}

	void FinishBlock()
	{
		state = AnimationState.FinishBlock;
		swordControl.FinishBlock();
		Debug.Log("FinishBlock");
	}

	void ChangeDirection()
	{
		swordControl.ChangeDirection(GetDirection());
	}


	Vector2 GetDirection()
	{
		Vector2 direction = Input.mousePosition;
		direction.x /= (float)Screen.width;
		direction.x -= 0.5f;
		direction.y /= (float)Screen.height;
		return direction;
	}


	void OnFinishHit()
	{
		state = AnimationState.Nothing;
		Debug.Log("OnFinishHit");
	}

	void OnFinishAnimation()
	{
	}

}

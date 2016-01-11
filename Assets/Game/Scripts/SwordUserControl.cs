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
		bool isSwingHold = (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E)) /*&& !isBlock*/;
		bool isHit = Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E);
		bool isBlock = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space);
		bool isBlockHold = (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space)) /*&& !isBlock*/;
		bool isBlockFinish = Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space);
		isRunning = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

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
				break;
			case AnimationState.Block:
				//Can finish block, swing, change direction
				ChangeDirection();
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
				state = AnimationState.Nothing;
				break;
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




//	SwordControl swordControl;
//	bool animateSwing = false;
//	bool animateHit = false;
//	bool animateBlock = false;
//
//	bool isRun = false;
//
//	Vector2 direction = Vector2.zero;
//	System.Action actionOnFinisHit;
//
//	void Awake()
//	{
//		swordControl = GetComponent<SwordControl>();
//	}
//
//	void Update()
//	{
//		isRun = false;
//		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
//		{
//			isRun = true;
//		}
//
//		if (!animateSwing && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
//		{
//			//Swing
//			Swing();
//
//		}
//		else if (animateHit && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
//		{
//			//Swing later
//			actionOnFinisHit += Swing;
//		}
//
//		bool hitNow = false;
//
//		if (animateSwing && !animateHit && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
//		{
//			//Hit
//			Hit();
//			hitNow = true;
//		}
//		else if (animateSwing && !hitNow && (!Input.GetKey(KeyCode.E) && !Input.GetMouseButton(0)))
//		{
//			//Also Hit
//			Hit();
//			hitNow = true;
//		}
//
//		if (animateHit && !hitNow && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
//		{
//			//Hit later
//			actionOnFinisHit += Hit;
//		}
//
//
//		if (!animateHit && !animateBlock && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)))
//		{
//			//Block
//			Block();
//		}
//
//
//		if (animateBlock && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(1)))
//		{
//			//Finish block
//			FinishBlock();
//		}
//
//
//
//		if ((animateSwing || animateBlock) && !animateHit)
//		{
//			//Change direction
//			direction = GetDirection();
//			swordControl.ChangeDirection(direction);
//		}
//
//		if (isRun && (animateHit || animateSwing || animateBlock))
//		{
//			swordControl.BeginRunning();
//		}
//		else if (!isRun && (animateHit || animateSwing || animateBlock))
//		{
//			swordControl.StopRunning();
//		}
//	}
//
//	private void Swing()
//	{
//		animateSwing = true;
//		animateBlock = false;
//
//		direction = GetDirection();
//		swordControl.Swing(direction, isRun, OnFinishHit);
//	}
//
//	private void Hit()
//	{
//		animateHit = true;
//
//		swordControl.Hit(direction, isRun);
//	}
//
//	private void Block()
//	{
//		animateSwing = false;
//		animateBlock = true;
//		direction = GetDirection();
//
//		swordControl.Block(direction, isRun);
//	}
//
//	private void FinishBlock()
//	{
//		animateBlock = false;
//		swordControl.FinishBlock();
//	}
//
//	private Vector2 GetDirection()
//	{
//		Vector2 direction = Input.mousePosition;
//		direction.x /= (float)Screen.width;
//		direction.x -= 0.5f;
//		direction.y /= (float)Screen.height;
//		return direction;
//	}
//
//	void OnFinishHit()
//	{
//		animateSwing = false;
//		animateHit = false;
//
//		if (actionOnFinisHit != null)
//		{
//			actionOnFinisHit();
//		}
//		actionOnFinisHit = null;
//	}

}

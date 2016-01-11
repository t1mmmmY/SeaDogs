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

	[SerializeField] float horizontalMouseRadius = 1.0f;
	[SerializeField] float verticalMouseRadius = 3.0f;

	SwordControl swordControl;

	AnimationState state;
	bool isRunning = false;
	Vector2 oldMousePosition;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
		state = AnimationState.Nothing;
		DisableOldDirection();

		Cursor.visible = false;
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

		DisableOldDirection();
	}

	void ChangeDirection()
	{
		Vector2 direction = GetDirection();
		if (direction != Vector2.zero)
		{
			//			Debug.Log(direction.ToString());
			swordControl.ChangeDirection(direction);
		}
	}


	Vector2 GetDirection()
	{
		Vector2 direction = Input.mousePosition;
		Vector2 shift = direction - oldMousePosition;

		oldMousePosition = direction;


		shift.x = shift.x / (float)Screen.width * horizontalMouseRadius;
		shift.y = shift.y / (float)Screen.height * verticalMouseRadius;

		return shift;
	}


	void OnFinishHit()
	{
		state = AnimationState.Nothing;
		Debug.Log("OnFinishHit");

		DisableOldDirection();
	}

	void OnFinishAnimation()
	{
	}

	void DisableOldDirection()
	{
		oldMousePosition = new Vector2(Screen.width / 2, Screen.height / 2);
	}

}

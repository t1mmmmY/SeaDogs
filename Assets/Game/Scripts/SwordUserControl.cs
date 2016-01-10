using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	SwordControl swordControl;
	bool animateSwing = false;
	bool animateHit = false;
	bool animateBlock = false;

	bool isRun = false;

	Vector2 direction = Vector2.zero;
	System.Action actionOnFinisHit;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
	}

	void Update()
	{
		isRun = false;
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			isRun = true;
		}

		if (!animateSwing && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
		{
			//Swing
			Swing();

		}
		else if (animateHit && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
		{
			//Swing later
			actionOnFinisHit += Swing;
		}

		bool hitNow = false;

		if (animateSwing && !animateHit && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
		{
			//Hit
			Hit();
			hitNow = true;
		}
		else if (animateSwing && !hitNow && (!Input.GetKey(KeyCode.E) && !Input.GetMouseButton(0)))
		{
			//Also Hit
			Hit();
			hitNow = true;
		}

		if (animateHit && !hitNow && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
		{
			//Hit later
			actionOnFinisHit += Hit;
		}


		if (!animateHit && !animateBlock && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)))
		{
			//Block
			Block();
		}


		if (animateBlock && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(1)))
		{
			//Finish block
			FinishBlock();
		}



		if ((animateSwing || animateBlock) && !animateHit)
		{
			//Change direction
			direction = GetDirection();
			swordControl.ChangeDirection(direction);
		}

		if (isRun && (animateHit || animateSwing || animateBlock))
		{
			swordControl.BeginRunning();
		}
		else if (!isRun && (animateHit || animateSwing || animateBlock))
		{
			swordControl.StopRunning();
		}
	}

	private void Swing()
	{
		animateSwing = true;
		animateBlock = false;

		direction = GetDirection();
		swordControl.Swing(direction, isRun, OnFinishHit);
	}

	private void Hit()
	{
		animateHit = true;

		swordControl.Hit(direction, isRun);
	}

	private void Block()
	{
		animateSwing = false;
		animateBlock = true;
		direction = GetDirection();

		swordControl.Block(direction, isRun);
	}

	private void FinishBlock()
	{
		animateBlock = false;
		swordControl.FinishBlock();
	}

	private Vector2 GetDirection()
	{
		Vector2 direction = Input.mousePosition;
		direction.x /= (float)Screen.width;
		direction.x -= 0.5f;
		direction.y /= (float)Screen.height;
		return direction;
	}

	void OnFinishHit()
	{
		animateSwing = false;
		animateHit = false;

		if (actionOnFinisHit != null)
		{
			actionOnFinisHit();
		}
		actionOnFinisHit = null;
	}

}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	SwordControl swordControl;
	bool animateSwing = false;
	bool animateHit = false;
	bool animateBlock = false;

	//Temp
	Vector2 direction = Vector2.zero;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
	}

	void Update()
	{
		bool isRun = false;
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			isRun = true;
		}

		if (!animateSwing && Input.GetMouseButtonDown(0))
		{
			//Swing

			animateSwing = true;
			animateBlock = false;

			direction = GetDirection();
			swordControl.Swing(direction, isRun, OnFinishHit);
		}
		if (animateSwing && !animateHit && Input.GetMouseButtonUp(0))
		{
			//Hit

			animateHit = true;

			swordControl.Hit(direction, isRun);
		}

		if (!animateHit && !animateBlock && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)))
		{
			//Block
			animateSwing = false;
			animateBlock = true;
			direction = GetDirection();

			swordControl.Block(direction, isRun);
		}
		if (animateBlock && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(1)))
		{
			//Finish block
			animateBlock = false;
			swordControl.FinishBlock();
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

	private Vector2 GetDirection()
	{
		Vector2 direction = Input.mousePosition;
//		direction.x /= (float)Screen.width;
		direction.x = 0.5f;
		direction.y /= (float)Screen.height;
		return direction;
	}

	void OnFinishHit()
	{
		animateSwing = false;
		animateHit = false;
	}

}

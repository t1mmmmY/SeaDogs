using UnityEngine;
using System.Collections;

public class SwordUserControl : SwordBaseControl 
{
	[SerializeField] float horizontalMouseRadius = 1.0f;
	[SerializeField] float verticalMouseRadius = 3.0f;


	Vector2 oldMousePosition;

	protected override void Awake()
	{
		base.Awake();

		DisableOldDirection();
		Cursor.visible = false;
	}


	protected override void Update()
	{
		isSwing = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
		isSwingHold = (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E));
		isHit = Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E);
		isBlock = Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space);
		isBlockHold = (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space));
		isBlockFinish = Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space);
		runNow = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

		base.Update();
	}

	protected override void FinishBlock()
	{
		base.FinishBlock();

		DisableOldDirection();
	}

	protected override void OnFinishHit()
	{
		base.OnFinishHit();

		DisableOldDirection();
	}

	protected override void ChangeDirection()
	{
		Vector2 direction = GetDirection();
		if (direction != Vector2.zero)
		{
			//			Debug.Log(direction.ToString());
			swordControl.ChangeDirection(direction);
		}

		base.ChangeDirection();
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

	void DisableOldDirection()
	{
		oldMousePosition = new Vector2(Screen.width / 2, Screen.height / 2);
	}

}

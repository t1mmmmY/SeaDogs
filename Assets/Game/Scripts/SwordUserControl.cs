using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	SwordControl swordControl;
	bool animateSwing = false;
	bool animateHit = false;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
	}

	void Update()
	{
		bool isRun = false;
		if (Input.GetAxis("Vertical") != 0)
		{
			isRun = true;
		}

		if (!animateSwing && Input.GetMouseButtonDown(0))
		{
			animateSwing = true;
			swordControl.Swing(new Vector2(0.5f, 1f), isRun, OnFinishHit);
		}
		if (animateSwing && !animateHit && Input.GetMouseButtonUp(0))
		{
			animateHit = true;
			swordControl.Hit(new Vector2(0.5f, 1f), isRun);
		}

		if (isRun && (animateHit || animateSwing))
		{
			swordControl.BeginRunning();
		}
	}

	void OnFinishHit()
	{
		animateSwing = false;
		animateHit = false;
	}

}

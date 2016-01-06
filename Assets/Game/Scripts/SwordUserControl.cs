using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	SwordControl swordControl;
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

		if (Input.GetMouseButtonDown(0))
		{
			animateHit = true;
			swordControl.Hit(new Vector2(0.5f, 1f), isRun, OnFinishHit);
		}

		if (isRun && animateHit)
		{
			swordControl.BeginRunning();
		}
	}

	void OnFinishHit()
	{
		animateHit = false;
	}

}

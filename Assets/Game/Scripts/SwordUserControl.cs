using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SwordControl))]
public class SwordUserControl : MonoBehaviour 
{
	SwordControl swordControl;

	void Awake()
	{
		swordControl = GetComponent<SwordControl>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			swordControl.Hit(new Vector2(0.5f, 1f));
		}
	}

}

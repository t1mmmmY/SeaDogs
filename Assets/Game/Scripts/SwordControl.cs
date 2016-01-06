using UnityEngine;
using System.Collections;

public enum HitType
{
	RightHit,
	LeftHit
}

public class SwordControl : MonoBehaviour 
{
	[SerializeField] Animator userAnimator;
	int rightHitCode = Animator.StringToHash("rightHit");
	int leftHitCode = Animator.StringToHash("leftHit");
	int hitAngleCode = Animator.StringToHash("hitAngle");

	void Awake()
	{
		userAnimator = GetComponent<Animator>();
	}

	public void Hit(Vector2 swingDirection)
	{
		HitType hitType = HitType.RightHit;
		if (swingDirection.x < 0)
		{
			hitType = HitType.LeftHit;
		}
		else
		{
			hitType = HitType.RightHit;
		}

		float angle = swingDirection.y;
		userAnimator.SetFloat(hitAngleCode, angle);

		switch (hitType)
		{
			case HitType.LeftHit:
				userAnimator.SetTrigger(leftHitCode);
				break;
			case HitType.RightHit:
				userAnimator.SetTrigger(rightHitCode);
				break;
		}
	}
}

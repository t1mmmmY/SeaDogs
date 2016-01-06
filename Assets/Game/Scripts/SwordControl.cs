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
	int rightHitRunCode = Animator.StringToHash("rightHitRun");
	int leftHitRunCode = Animator.StringToHash("leftHitRun");
	int rightHitStayCode = Animator.StringToHash("rightHitRun");
	int leftHitStayCode = Animator.StringToHash("leftHitRun");
	int hitAngleCode = Animator.StringToHash("hitAngle");
	System.Action onFinishHit;

	void Awake()
	{
		if (userAnimator == null)
		{
			userAnimator = GetComponent<Animator>();
		}
	}


	public void Hit(Vector2 swingDirection, bool isRun, System.Action callback)
	{
		if (isRun)
		{
			userAnimator.SetLayerWeight(0, 0.0f);
			userAnimator.SetLayerWeight(1, 1.0f);
			userAnimator.SetLayerWeight(2, 1.0f);
			userAnimator.SetLayerWeight(3, 0.0f);
		}
		else
		{
			userAnimator.SetLayerWeight(0, 0.0f);
			userAnimator.SetLayerWeight(1, 0.0f);
			userAnimator.SetLayerWeight(2, 0.0f);
			userAnimator.SetLayerWeight(3, 1.0f);
		}

		onFinishHit = callback;

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
				userAnimator.SetTrigger(isRun ? leftHitRunCode : leftHitStayCode);
				break;
			case HitType.RightHit:
				userAnimator.SetTrigger(isRun ? rightHitRunCode : rightHitStayCode);
				break;
		}
	}

	public void BeginRunning()
	{
		userAnimator.SetLayerWeight(0, 0.0f);
		userAnimator.SetLayerWeight(1, 1.0f);
		userAnimator.SetLayerWeight(2, 1.0f);
		userAnimator.SetLayerWeight(3, 0.0f);
	}

	public void OnFinishHit()
	{
		userAnimator.SetLayerWeight(0, 1.0f);
		userAnimator.SetLayerWeight(1, 0.0f);
		userAnimator.SetLayerWeight(2, 0.0f);
		userAnimator.SetLayerWeight(3, 0.0f);

		if (onFinishHit != null)
		{
			onFinishHit();
		}
	}
}

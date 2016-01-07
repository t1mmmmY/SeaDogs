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


	int rightSwingCode = Animator.StringToHash("rightSwing");
	int rightHitCode = Animator.StringToHash("rightHit");


	System.Action onFinishHitCallback;

	void Awake()
	{
		if (userAnimator == null)
		{
			userAnimator = GetComponent<Animator>();
		}
	}

	public void Swing(Vector2 swingDirection, bool isRun, System.Action callback)
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

		onFinishHitCallback = callback;

		HitType hitType = GetHitType(swingDirection);

		float angle = swingDirection.y;
		userAnimator.SetFloat(hitAngleCode, angle);

		switch (hitType)
		{
			case HitType.LeftHit:
				Debug.LogError("Not implemented yet");
//				userAnimator.SetTrigger(rightSwingCode);
				break;
			case HitType.RightHit:
				userAnimator.SetTrigger(rightSwingCode);
				break;
		}
	}

	public void Hit(Vector2 hitDirection, bool isRun)
	{
		HitType hitType = GetHitType(hitDirection);

		float angle = hitDirection.y;
		userAnimator.SetFloat(hitAngleCode, angle);

		switch (hitType)
		{
			case HitType.LeftHit:
				Debug.LogError("Not implemented yet");
//				userAnimator.SetTrigger(isRun ? leftHitRunCode : leftHitStayCode);
				break;
			case HitType.RightHit:
				userAnimator.SetTrigger(rightHitCode);
//				userAnimator.SetTrigger(isRun ? rightHitRunCode : rightHitStayCode);
				break;
		}
	}

	private HitType GetHitType(Vector2 direction)
	{
		return direction.x < 0 ? HitType.LeftHit : HitType.RightHit;
	}

	public void BeginRunning()
	{
		userAnimator.SetLayerWeight(0, 0.0f);
		userAnimator.SetLayerWeight(1, 1.0f);
		userAnimator.SetLayerWeight(2, 1.0f);
		userAnimator.SetLayerWeight(3, 0.0f);
	}

	public void OnHitDone()
	{
		if (onFinishHitCallback != null)
		{
			onFinishHitCallback();
		}
	}

	public void OnFinishAnimation()
	{
		userAnimator.SetLayerWeight(0, 1.0f);
		userAnimator.SetLayerWeight(1, 0.0f);
		userAnimator.SetLayerWeight(2, 0.0f);
		userAnimator.SetLayerWeight(3, 0.0f);
	}
}

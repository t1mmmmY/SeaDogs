using UnityEngine;
using System.Collections;

public enum HitType
{
	RightHit,
	LeftHit
}

public enum AnimationStatus
{
	Idle,
	HitStay,
	HitRunning
}

public class SwordControl : MonoBehaviour 
{
	[SerializeField] Animator userAnimator;
	int hitAngleCode = Animator.StringToHash("hitAngle");
	int hitDirectionCode = Animator.StringToHash("hitDirection");

	int swingCode = Animator.StringToHash("swing");
	int hitCode = Animator.StringToHash("hit");
	int blockCode = Animator.StringToHash("block");
	int blockFinishCode = Animator.StringToHash("blockFinish");


	bool attack = false;

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
		attack = true;

		if (isRun)
		{
			ChangeLayersWeight(AnimationStatus.HitRunning);
		}
		else
		{
			ChangeLayersWeight(AnimationStatus.HitStay);
		}

		onFinishHitCallback = callback;

		HitType hitType = GetHitType(swingDirection);

		ChangeDirection(swingDirection);

		userAnimator.SetTrigger(swingCode);
		userAnimator.ResetTrigger(hitCode);
	}

	public void Hit(Vector2 hitDirection, bool isRun)
	{
		HitType hitType = GetHitType(hitDirection);

		userAnimator.SetTrigger(hitCode);

	}

	public void Block(Vector2 blockDirection, bool isRun)
	{
		if (isRun)
		{
			ChangeLayersWeight(AnimationStatus.HitRunning);
		}
		else
		{
			ChangeLayersWeight(AnimationStatus.HitStay);
		}

		HitType hitType = GetHitType(blockDirection);
		ChangeDirection(blockDirection);
		userAnimator.SetTrigger(blockCode);
	}

	public void FinishBlock()
	{
		userAnimator.SetTrigger(blockFinishCode);

		ChangeLayersWeight(AnimationStatus.Idle);
	}

	public void ChangeDirection(Vector2 newDirection)
	{
		userAnimator.SetFloat(hitAngleCode, newDirection.y);
		userAnimator.SetFloat(hitDirectionCode, newDirection.x);

	}

	private HitType GetHitType(Vector2 direction)
	{
		return direction.x < 0 ? HitType.LeftHit : HitType.RightHit;
	}

	public void BeginRunning()
	{
		ChangeLayersWeight(AnimationStatus.HitRunning);
	}

	public void StopRunning()
	{
		ChangeLayersWeight(AnimationStatus.HitStay);
	}

	public void OnHitDone()
	{
		attack = false;

		if (onFinishHitCallback != null)
		{
			onFinishHitCallback();
		}
	}

	public void OnFinishAnimation()
	{
		if (!attack)
		{
			ChangeLayersWeight(AnimationStatus.Idle);
		}
	}

	private void ChangeLayersWeight(AnimationStatus status)
	{
		switch (status)
		{
			case AnimationStatus.Idle:
				userAnimator.SetLayerWeight(0, 1.0f);
				userAnimator.SetLayerWeight(1, 0.0f);
				userAnimator.SetLayerWeight(2, 0.0f);
				userAnimator.SetLayerWeight(3, 0.0f);
				break;
			case AnimationStatus.HitStay:
				userAnimator.SetLayerWeight(0, 0.0f);
				userAnimator.SetLayerWeight(1, 0.0f);
				userAnimator.SetLayerWeight(2, 0.0f);
				userAnimator.SetLayerWeight(3, 1.0f);
				break;

			case AnimationStatus.HitRunning:
				userAnimator.SetLayerWeight(0, 0.0f);
				userAnimator.SetLayerWeight(1, 1.0f);
				userAnimator.SetLayerWeight(2, 1.0f);
				userAnimator.SetLayerWeight(3, 0.0f);
				break;
		}
	}
}

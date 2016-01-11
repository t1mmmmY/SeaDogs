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

	int canAnimateCode = Animator.StringToHash("CanAnimate");
	int swingCode = Animator.StringToHash("Swing");
	int hitCode = Animator.StringToHash("Hit");
	int blockCode = Animator.StringToHash("Block");
	int blockFinishCode = Animator.StringToHash("FinishBlock");


	bool attack = false;

	System.Action onFinishHitCallback;

	void Awake()
	{
		if (userAnimator == null)
		{
			userAnimator = GetComponent<Animator>();
		}

		userAnimator.SetBool(canAnimateCode, true);
	}

	public void Swing(bool isRun)
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

		userAnimator.SetBool(blockCode, false);
		userAnimator.SetBool(swingCode, true);
	}

	public void Hit(bool isRun, System.Action callback)
	{
		attack = true;

		onFinishHitCallback = callback;

		userAnimator.SetBool(hitCode, true);
		userAnimator.SetBool(canAnimateCode, false);
	}

	public void Block(bool isRun)
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

		userAnimator.SetBool(swingCode, false);
		userAnimator.SetBool(blockCode, true);
	}

	public void FinishBlock()
	{
		userAnimator.SetBool(blockCode, false);

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
		if (attack)
		{
			userAnimator.SetBool(swingCode, false);
			userAnimator.SetBool(hitCode, false);
			userAnimator.SetBool(canAnimateCode, true);

			if (onFinishHitCallback != null)
			{
				onFinishHitCallback();
			}
		}

		attack = false;
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

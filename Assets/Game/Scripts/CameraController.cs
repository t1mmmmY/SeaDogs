using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraController : MonoBehaviour 
{
	[SerializeField] Transform target;
	[SerializeField] Transform cameraTransform;
	[SerializeField] Transform targetHead;
	[SerializeField] Transform mainCamera;

	[SerializeField] float speed = 1.0f;
	[SerializeField] float speedRotate = 1.0f;
	[SerializeField] float sensetivity = 1.0f;
	[SerializeField] Vector2 maxVerticalAngles = Vector2.zero;

//	[SerializeField] bool targeting = false;
	bool focused = false;

	Tweener lookAtTweener;

	void Update()
	{
		transform.DOMove(target.position, speed);
		cameraTransform.DOMove(targetHead.position, speed);

		Vector2 rotationAngle = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * sensetivity;

		Vector3 currentAngles = cameraTransform.rotation.eulerAngles;
		currentAngles.x += rotationAngle.y;
		currentAngles.y += rotationAngle.x;
		currentAngles.z = 0;


		currentAngles.x = ClampAngle(currentAngles.x, maxVerticalAngles.x, maxVerticalAngles.y);

		cameraTransform.rotation = Quaternion.Euler(currentAngles);


//		if (targetObject == null)
//		{
//		}
//		else
//		{
//			lookAtTweener = cameraTransform.DOLookAt(targetObject.center.position, 0.2f);
//		}

	}

	float ClampAngle(float angle, float min, float max)
	{

		if (angle < 90 || angle > 270)
		{       // if angle in the critic region...
			if (angle > 180) angle -= 360;  // convert all angles to -180..+180
			if (max > 180) max -= 360;
			if (min > 180) min -= 360;
		}    
		angle = Mathf.Clamp(angle, min, max);
		if (angle < 0) angle += 360;  // if angle negative, convert to 0..360

		return angle;
	}

}

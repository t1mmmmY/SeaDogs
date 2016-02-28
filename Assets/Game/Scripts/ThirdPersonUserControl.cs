using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
	[SerializeField] float sensetivity = 2.0f;
	[SerializeField] Transform cameraTransform;
	[SerializeField] Transform mainCamera;
	[SerializeField] ObjectToHit thisObjectToHit;

	[SerializeField] Kino.Bokeh focusEffect;

    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

	[SerializeField] float fieldOfViewRange = 30;

	ObjectToHit targetObject = null;
	Tweener lookAtTweener;

    
    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
    }


    // Fixed update is called in sync with physics
    private void Update()
    {
        // read inputs
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = vertical * m_CamForward + horizontal * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
			m_Move = vertical * Vector3.forward + horizontal * Vector3.right;
        }
#if !MOBILE_INPUT
		// walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif


		if (Input.GetKeyDown(KeyCode.F) && targetObject == null)
		{
			//Focus on target
			targetObject = Raycaster.FindClosestTarget(mainCamera, 10, fieldOfViewRange, thisObjectToHit);
			//			Debug.Log("Focus on " + targetObject.ToString());
			if (targetObject != null && targetObject != thisObjectToHit)
			{
				m_Character.FocusOnTarget(targetObject.center);

				focusEffect.enabled = true;
				focusEffect.subject = targetObject.center.transform;
				StopCoroutine("SetFocus");
				StartCoroutine("SetFocus", new Vector2(10, 2));
				//Debug.Log("target " + targetObject.name);
			}
		}
		else if (Input.GetKeyDown(KeyCode.F) && targetObject != null)
		{
			//Unfocus
			targetObject = null;
			m_Character.Unfocus();

			StopCoroutine("SetFocus");
			StartCoroutine("SetFocus", new Vector2(2, 10));


			if (lookAtTweener != null)
			{
				lookAtTweener.Kill();
			}
		}

        // pass all parameters to the character control script
		m_Character.Move(m_Move, m_Jump, targetObject != null);
        m_Jump = false;

    }

	IEnumerator SetFocus(Vector2 value)
	{
		float elapsedTime = 0;
		do
		{
			elapsedTime += Time.deltaTime;

			focusEffect.fNumber = Mathf.Lerp(value.x, value.y, elapsedTime);

			yield return new WaitForEndOfFrame();

		} while (elapsedTime < 1.0f);

		if (value.y == 10)
		{
			focusEffect.enabled = false;
		}
	}

	void LateUpdate()
	{
		if (targetObject != null)
		{
			if (cameraTransform != null)
			{
				lookAtTweener = cameraTransform.DOLookAt(targetObject.center.position, 0.2f);
			}
		}
	}
}

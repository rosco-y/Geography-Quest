using UnityEngine;
using System.Collections;

// a basic third person character controller that we will extend and expand upon
// inspired from thirdpersoncontroller.js, by Unity Technologies

public class playerControls : MonoBehaviour {

	public Vector3 initialPosition;
	public Vector3 moveDirection = Vector3.zero;
	public float rotateSpeed;
	private float movespeed = 0.0f;
	public float speedSmoothing = 10.0f;
	public float maxSpeed;
	public float gravity;
	public Animator _animator;
	
	public GameObject hat;
	public GameObject body;
	
	// cached state hashes for detecting state changes
	private int idleState;
	private int runState;

	// Use this for initialization
	void Start () {
		this.transform.position = initialPosition;
		
		if (_animator)
		{
			idleState = Animator.StringToHash("Base Layer.Idle");    
			runState = Animator.StringToHash("Base Layer.Run");
		}
	}
	
	public void EnablePlayerRenderer(bool renderflag)
	{
		if (body)
		{
			body.GetComponent<SkinnedMeshRenderer>().enabled = renderflag;
		}
	}

	void updateMovement()
	{
		// We want our controls to be relative to the camera.  So we will need to know it's orientation
		// 1) find the horizontally aligned 'forward' and 'right' vectors of the camera.
		Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
		cameraForward.y = 0.0f;
		cameraForward.Normalize();
		Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);
		
		// 2) grab the raw axis data from the keyboard/joypad
		// this data is in range [-1, 1].
		// for h [-1,1] corresponds to full left, full right
		// for v [-1, 1] corresponds to full down, full up
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		// 3) compute the new 'camera relative' direction vector for our player
		// as the sum of v and h in camera realtive space.
		Vector3 targetDirection = h * cameraRight + v * cameraForward;

		// 4) we always will smoothly adjust the actual facing of the player (moveDirection)
		// to the target direction (computed from user input in 3).  This way, the player always walks forward
		if (targetDirection != Vector3.zero)
		{
			moveDirection = Vector3.RotateTowards (moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			moveDirection = moveDirection.normalized;
		}
		
		// 5) we always will smoothly interpolate the speed of the player from previous speed (movespeed)
		// to the target speed (computed directly as the magnitude of the inputs from the user).  On a keyboard
		// this may be harder to see, but on a anlog stick this gives a nice effect that can later be used
		// to drive walk/run animations
		float curSmooth = speedSmoothing * Time.deltaTime;
		float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
		movespeed = Mathf.Lerp (movespeed, targetSpeed*maxSpeed, curSmooth);

		if (_animator)
		{
			_animator.SetFloat ("speed",movespeed);

			// if it is in RUN, then disable it
		}

		// scale the movedirection * speed to compute the actual displacement this frame
		// (as a function of elapsed time, so it's not framerate dependent)
		Vector3 displacement = moveDirection * movespeed * Time.deltaTime;
		displacement.y = -gravity * Time.deltaTime;
		
		// finally, apply the translation via the character controller component method 'move'
		// finally, assign the lerped rotation to the player transform so that the player rotates
		// towards the lerped direction
		GetComponent<CharacterController>().Move (displacement);
		
		RaycastHit hitinfo;
		Ray r = new Ray(this.transform.position, -Vector3.up);
		Physics.Raycast( r, out hitinfo);
		this.transform.position = new Vector3(this.transform.position.x, hitinfo.point.y + (this.GetComponent<Collider>() as CapsuleCollider).height, this.transform.position.z);

		transform.rotation = Quaternion.LookRotation (moveDirection);
	}
	
	// Update is called once per frame
	void Update () {

		updateMovement();
	}
}

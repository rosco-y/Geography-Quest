using UnityEngine;
using System.Collections;

public class GameCam : MonoBehaviour {
	
	public GameObject trackObj;
	public GameObject lookObj;
	public float height;
	public float desiredDistance;
	public float rotDamp;
	public float heightDamp;
	public float kh;

	// Use this for initialization
	void Start ()
	{
		// cache object references and initialize class variables here
	}
	
	public void LookUp()
	{
		GameObject go = GameObject.Find ("lookupTarget");
		if (go)
			lookObj = go;
	}
	
	public void LookPlayer()
	{
		if (trackObj)
			lookObj = trackObj;
	}

	void updateRotAndTrans()
	{
		if (trackObj)
		{
			// the camera will strive to be behind the object (angle y), and to be
			// at height h
			float DesiredRotationAngle = trackObj.transform.eulerAngles.y;
			float DesiredHeight = trackObj.transform.position.y + height;
			
			// these 2 variables are the current rotation and height of the transform
			float RotAngle = transform.eulerAngles.y;
			float Height = transform.position.y;
			
			// every frame, we will slowly interpolate from the current rotation and height
			// towards to target rotation and height (the rot and height of the obj. we are tracking)
			// if rotDamp is large enough, this will result in a camera 'glued' behind the trackObj
			// if heightDamp is large enough, this will result in a camera 'locked' in y to the value 'height'
			RotAngle = Mathf.LerpAngle (RotAngle, DesiredRotationAngle, rotDamp);
			Height = Mathf.Lerp (Height, DesiredHeight, heightDamp * Time.deltaTime);
			
			// we convert the euler angle representation of our rotation to Quaternion
			// and compute the camera offset as the rotation Q * - facing vector * distance
			//
			// the result is a position behind the object at distance 'desiredDistance'
			Quaternion CurrentRotation = Quaternion.Euler (0.0f, RotAngle, 0.0f);	
			Vector3 desiredPos = trackObj.transform.position;
			desiredPos -= CurrentRotation * Vector3.forward * desiredDistance;
			desiredPos.y = Height;
			transform.position += kh * (desiredPos - transform.position);

			// setting transform.lookAt ensures that the camera always looks at the center
			// of the trackObject.  This gives a nice effect with the lagging position
			// calcuation above
			transform.LookAt (lookObj.transform.position);
		}
		else
			Debug.Log ("GameCam::Error, no trackObj reference in Inspector. Please add object to track");
	}

	// Update is called once per frame
	void Update () {
		updateRotAndTrans();
	}
}

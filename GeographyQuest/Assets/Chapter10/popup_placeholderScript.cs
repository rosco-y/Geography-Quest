using UnityEngine;
using System.Collections;

// the script that will look down at the player.  used by the popup system
public class popup_placeholderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject camObj = GameObject.Find ("MainCamera");
		if (camObj)
		{
			camObj.GetComponent<GameCam>().LookPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

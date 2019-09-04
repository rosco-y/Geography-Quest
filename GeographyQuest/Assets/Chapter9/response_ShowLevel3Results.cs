using UnityEngine;
using System.Collections;

// an npc response script to display the appropriate level3 results popop
[System.Serializable]
public class response_ShowLevel3Results: npcResponse {

	public GameObject passPopup;

	public override bool dispatch()
	{
		bool rval = true;
		if (passPopup != null)
			passPopup.SetActive(true);
		else 
			Debug.Log("response_ShowLevel3Results:  error, passpopup is null");

		GameObject cameraObj = GameObject.Find ("MainCamera");
		if (cameraObj)
		{
			cameraObj.GetComponent<GameCam>().LookUp ();
		}
		GameObject clock = GameObject.Find ("Time");
		if (clock)
		{
			clock.SetActive (false);
		}
		return rval;
	}
}

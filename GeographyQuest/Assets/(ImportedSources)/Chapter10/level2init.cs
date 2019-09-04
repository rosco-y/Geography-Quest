using UnityEngine;
using System.Collections;

// the class that initializes level 2, moves the player to the correct starting position
public class level2init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GameObject playerObj = GameObject.Find ("Player1");
		if (playerObj == null)
			playerObj = GameObject.Find ("Player");
		if (playerObj != null)
		{
			playerObj.transform.position = new Vector3(-110.0f, 3.0f, 166.0f);
			playerObj.GetComponent<playerControls>().moveDirection = new Vector3(1.0f, 0.0f, 0.0f);
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		GameObject camObj = GameObject.Find ("MainCamera");
		if (camObj)
		{
			PopupMgr ppm = camObj.GetComponent<PopupMgr>();
			if (ppm)
			{
				// setup the level2 popups initial state
				ppm.Level2Finish.SetActive(false);
				ppm.Level2Repeat.SetActive (false);
				ppm.Level2Start.SetActive(true);
				PopupButtonScript pbs = ppm.Level2Start.transform.Find ("Button1").gameObject.GetComponent<PopupButtonScript>();
			
				Level2Extras l2x = GetComponent<Level2Extras>();
				if (l2x)
				{
					//setup the extras as the objects as objects that can be operated on in the popup_level2start button's
					//action list.  this will cause the setupLevel2 and raceStartup objects will be enabled on click
					pbs.actions[0].data.obj = l2x.setupLevel2;
					pbs.actions[1].data.obj = l2x.raceStartup;
					
					response_ShowRaceResultsPopup rrp = l2x.LevelLogicObj.GetComponent<response_ShowRaceResultsPopup>();
					if (rrp)
					{
						// connect the level2Finish and level2Repeat popups to the showraceResultspopup response on the
						// levelLogicObj object.  This allows levelLogicObj to enable either the repeat or pass popup when
						// necessary
						rrp.player = GameObject.Find ("Player1");
						rrp.gm = GameObject.Find ("Game").GetComponent<gameMgr>();
						rrp.passPopup = ppm.Level2Finish;
						rrp.retryPopup = ppm.Level2Repeat;
					}

					Destroy (this);
				}
			}

		}
	}
}

using UnityEngine;
using System.Collections;

// the class that initializes level3
public class Level3init : MonoBehaviour {

	// Use this for initialization
	void Start () {
	GameObject go = GameObject.Find ("MainCamera");
		if (go)
		{
			// connect the lose ends between prefab instances in _level3 with objects in _global (such as popupmanager)
			PopupMgr ppm = go.GetComponent<PopupMgr>();
			if (ppm)
			{
				ppm.Level3Finish.SetActive (false);
				ppm.Level3Repeat.SetActive(false);
				ppm.Level3Start.SetActive(true);
				PopupButtonScript pbs = ppm.Level3Start.transform.Find ("Button1").gameObject.GetComponent<PopupButtonScript>();
			
				// connect the enable setup level 3 button to the actual gameobject
				Level3Extras l3x = GetComponent<Level3Extras>();
				if (l3x)
				{
					pbs.actions[0].data.obj = l3x.setupLevel3;
				}
				
				// connect the pass popup to the response behavior that shows the popup when you get enough points
				GameObject llo = GameObject.Find ("LevelLogicObj");
				if (llo != null)
				{
					llo.GetComponent<response_ShowLevel3Results>().passPopup = ppm.Level3Finish;
				}
				
				// connect the repeat popup to the time script for when the game clock ticks to zero
				GameObject TimeObj = GameObject.Find ("Time");
				if (TimeObj != null)
				{
					TimeObj.GetComponent<TimeScript>().failPopup = ppm.Level3Repeat;
				}
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupPanel : MonoBehaviour {

	public string popupName;
	public TextMesh nametxt;
	public bool positionOverride;
	public bool lookUp = false; //if true, calls camera.lookup
	public bool lookPlayer = false;
	public int id;
	public Vector3 pos;
	public Vector3 rot;
	private List<GameObject> buttons = new List<GameObject>();

	// Use this for initialization
	void Start () {
		nametxt.text = popupName;
		GameObject gameObj = GameObject.Find ("MainCamera");

		if (gameObj)
		{
			this.transform.parent = gameObj.transform;
			if (positionOverride)
			{
				this.transform.localPosition = pos;
				this.transform.localEulerAngles = rot;
			}		
			
			// adjust camera after attaching
			GameCam gameCamera = gameObj.GetComponent<GameCam>();
			if (gameCamera)
			{
				if (lookUp)
					gameCamera.LookUp();
				else if (lookPlayer)
					gameCamera.LookPlayer();
			}			
		}
	}
	
	void initButtons()
	{
		// find all child objects of type PopupButtonScript
		// store them in this list
		foreach (Transform transf in transform)
		{
			if (transf.gameObject.GetComponent<PopupButtonScript>() != null)
			{
				buttons.Add (transf.gameObject);
			}
		}
	}

	public void SetCorrectButtonPopup(GameObject newPopup)
	{
		
		initButtons();

		// for each button, loop over all interations
		// for each data member, if it has popup_placeholder tag, then replace it with the reference.
		foreach (GameObject gameObj in buttons)
		{
			PopupButtonScript popup = gameObj.GetComponent<PopupButtonScript>();
			if (popup)
				popup.SetPopup(newPopup);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

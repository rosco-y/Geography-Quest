using UnityEngine;
using System.Collections;

// a simple main menu class used in the 1st iteration of the menu system, to launc the first level when the user clicks on the panel.
// this class is replaced by the popup system developed later in the book
public class mainMenuScript : MonoBehaviour {
	
	private GameObject GameObj;
	private gameMgr gm;

	// Use this for initialization
	void Start () {
		GameObj = GameObject.Find ("Game");	
		if (GameObj)
		{
			gm = GameObj.GetComponent<gameMgr>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		//change state to level1.
		if (gm)
		{
			gm.SetState(gameMgr.eGameState.eGS_Level1);
			this.gameObject.SetActive(false);//enabled = false;
		}
	}
}

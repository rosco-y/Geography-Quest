using UnityEngine;
using System.Collections;

// a script that handles displaying the score (from playerData) onto a GUIText element on screen.
public class scoreScript : MonoBehaviour {

	private playerData pd;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("Player");
		if (go == null)
			go = GameObject.Find ("Player1");

		if (go)
		{
			pd = go.GetComponent<playerData>();
		}
	}
	
	// Update is called once per frame
	void OnGUI() {
		
		if (pd)
		{
		    //get score and display it in text field of GUIText element
			int score = pd.GetScore();
			this.gameObject.GetComponent<GUIText>().text = score.ToString ();
		}
		
	}
}

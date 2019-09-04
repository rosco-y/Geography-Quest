using UnityEngine;
using System.Collections;

// a script that implements a clock that counts down to zero.  Used in level3.
public class TimeScript : MonoBehaviour {
	
	public GameObject failPopup;
	public float starting_time;
	private float t;
	public bool timeElapsed = false;

	// Use this for initialization
	void Start () {
		t = starting_time;
		timeElapsed = false;
	}

	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;

		if (!timeElapsed)
		{
			if (t < 0.0f)
			{
				GameObject c = GameObject.Find ("MainCamera");
				if (c)
					c.GetComponent<GameCam>().LookUp();

				failPopup.SetActive(true);
				timeElapsed = true;
			}

			this.gameObject.GetComponent<GUIText>().text = "Time : "+((int)t).ToString ();
		}
	}
}

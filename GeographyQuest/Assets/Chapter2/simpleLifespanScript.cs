using UnityEngine;
using System.Collections;

// a simple helper script that will kill a GameObject after a specific elapsed time.  Good for displaying popups for specific time intervals.
public class simpleLifespanScript : MonoBehaviour {
	
	public float seconds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		seconds -= Time.deltaTime;
		if (seconds <= 0.0f)
			GameObject.Destroy (this.gameObject);
	}
}

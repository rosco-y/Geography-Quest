using UnityEngine;
using System.Collections;

// a simple class that contains an id for a mission objective, as well as a title and description.  In most (but not all) situations the id's
// should be unique.  title can be used to display a user appropriate string in game, and the description could be used internally by the designer
// to keep track of individual tokens
public class missionToken : MonoBehaviour {

	public int id;
	public string title;
	public string description;
	
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}

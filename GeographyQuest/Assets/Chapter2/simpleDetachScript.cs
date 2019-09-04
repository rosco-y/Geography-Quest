using UnityEngine;
using System.Collections;

// simple helper script ot detach an object from it's parent.  useful when a prefab is instantiated that needs to accomplish this
public class simpleDetachScript : MonoBehaviour {
	
	public Vector3 newPos;

	// Use this for initialization
	void Start () {
		this.transform.parent = null;
		this.transform.position = newPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

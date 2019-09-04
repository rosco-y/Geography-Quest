using UnityEngine;
using System.Collections;

// a simple helper script that associates a globally unique id with a GameObject
public class guid : MonoBehaviour {
	
	public int _guid;
	public System.Guid _id;

	// Use this for initialization
	void Start () {
		_id = System.Guid.NewGuid();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

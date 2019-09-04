using UnityEngine;
using System.Collections;

// a simple helper script that returns the ith child gameObject.  Useful for finding a gameobject from a private list.  Used in mission1 by momnumentMgr.
public class GetChildGameObj : MonoBehaviour {
	
	public GameObject go;
	public int index;
	private Transform[] gameobjects;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		gameobjects = GetComponentsInChildren<Transform>() as Transform[];
		go = gameobjects[index].gameObject;
	}
}

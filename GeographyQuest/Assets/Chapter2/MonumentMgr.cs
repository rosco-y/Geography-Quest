using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a class that handles the specific logic for the flag monument GameObject in mission1.  It provides an interface to attach something (a flag)
// to a specific flag mount.
public class MonumentMgr : MonoBehaviour {

	public List<GameObject> mountPoints;

	// Use this for initialization
	void Start () {
	}
	
	public void attachObjToMountPoint(GameObject go, int index)
	{
		GameObject newGo = (GameObject)Instantiate (go, mountPoints[index].transform.position, mountPoints[index].transform.rotation);
		newGo.SetActive(true);
		newGo.transform.parent = mountPoints[index].transform;
		newGo.transform.localPosition = Vector3.zero;
		newGo.transform.localEulerAngles = Vector3.zero;//mount.transform.eulerAngles;
	}
}
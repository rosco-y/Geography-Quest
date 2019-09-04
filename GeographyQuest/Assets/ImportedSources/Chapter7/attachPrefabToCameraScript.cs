using UnityEngine;
using System.Collections;

// simple helper script to attach an object to the camera
public class attachPrefabToCameraScript : MonoBehaviour {
	
	public GameObject prefab;
	public Vector3 offset;
	public Quaternion rot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject cam = Camera.main.gameObject;
		GameObject go = (GameObject)Instantiate (prefab, cam.transform.position + offset, cam.transform.localRotation );
		Destroy(this.gameObject);
	}
}

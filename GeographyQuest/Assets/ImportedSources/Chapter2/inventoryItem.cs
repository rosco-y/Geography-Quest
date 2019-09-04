using UnityEngine;
using System.Collections;

// a container for objects that can be displayed by the inventoryMgr
// a simple class that does not inherit from monobehavior
[System.Serializable]
public class inventoryItem {
	
	public Texture displayTexture = null;
	public GameObject	item = null;
	public GameObject 	popup = null;
	public int quantity = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

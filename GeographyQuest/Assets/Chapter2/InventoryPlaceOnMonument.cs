using UnityEngine;
using System.Collections;

// this helper class performs a specific role in mission 1.  It attaches a set of flag gameobjects to the monument's attachpoints.
public class InventoryPlaceOnMonument : MonoBehaviour {

	public int objectIndex;
	private inventoryMgr _inventoryMgr;
	private GameObject _monument;
	private bool attached;

	// Use this for initialization
	void Start () {
		_inventoryMgr = GameObject.Find ("Player").GetComponent<inventoryMgr>();
		_monument = GameObject.Find("Monument");
		attached = false;
	}

	// Update is called once per frame
	void Update () {
		GameObject go = _inventoryMgr.inventoryObjects[objectIndex].item;
		//go.SetActive (true);
		
		if ((_monument) && (attached == false))
		{
			_monument.GetComponent<MonumentMgr>().attachObjToMountPoint(go, objectIndex);
			attached = true;
		}

	}
}

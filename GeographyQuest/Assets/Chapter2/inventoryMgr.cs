using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// the class that implements the inventoryMgr system.  This allows the user to collect
// display and interact(click) with inventory objects.  An inventory object is created
// as an adaptor to an interactive object when the interactive object is collided with
public class inventoryMgr : MonoBehaviour {

	public List<inventoryItem> inventoryObjects = new List<inventoryItem>();
	public int numCells;
	public float height;
	public float width;
	public float yPosition;
	
	private missionMgr _missionMgr;

	// Use this for initialization
	void Start () {
		
		GameObject go = GameObject.Find ("Game");
		if (go)
			_missionMgr = go.GetComponent<missionMgr>();
	}
	
	void insert(interactiveObj iObj)
	{
		// slot into first available spot
		objectInteraction oi = iObj.OnCloseEnough;

		inventoryItem ii = new inventoryItem();
		ii.item = iObj.gameObject;
		ii.quantity = 1;
		ii.displayTexture = oi._tex;
		ii.item.SetActive (false);
		inventoryObjects.Add (ii);

		// add token from this object to missionMgr to track, it this obj has a token
		missionToken mt = ii.item.GetComponent<missionToken>();
		if (mt != null)
			_missionMgr.add(mt);
		
		// if there is a popupInfo, instantiate it on pickup
		Instantiate (ii.item.GetComponent<customGameObject>().popUpInfo, Vector3.zero, Quaternion.identity);
	}

	// based on the type of interaction component that the interactive object
	// has, the inventoryMgr will handle the add in a specific way
	public void Add(interactiveObj iObj)
	{
		objectInteraction oi = iObj.OnCloseEnough;

		switch(oi.interactionType)
		{
			case(objectInteraction.InteractionType.Unique):
			{
				// slot into first available spot
				insert(iObj);
			}
			break;

			case(objectInteraction.InteractionType.Accumulate):
			{
				bool inserted = false;

				// find object of same type, and increase
				customGameObject cgo = iObj.gameObject.GetComponent<customGameObject>();
				customGameObject.CustomObjectType ot = customGameObject.CustomObjectType.Invalid;
				if (cgo != null)
					ot = cgo.objectType;

				for (int i = 0; i < inventoryObjects.Count; i++)
				{
					//todo
					customGameObject cgoi = inventoryObjects[i].item.GetComponent<customGameObject>();
					customGameObject.CustomObjectType io = customGameObject.CustomObjectType.Invalid;
					if (cgoi != null)
						io = cgoi.objectType;

					if (ot == io)
					{
						inventoryObjects[i].quantity++;
						// add token from this object to missionMgr to track, it this obj has a token
						missionToken mt = iObj.gameObject.GetComponent<missionToken>();
						if (mt != null)
							_missionMgr.add(mt);

						iObj.gameObject.SetActive (false);
						inserted = true;
						break;
					}
				}
				
				//
				//	if we get this far, it means no duplicate found in the inventory, so let's insert it
				//
				if (!inserted)
					insert(iObj);
			}
			break;
		}
	}

	// iterates through the inventory and display inventory items
	// also gives opportunity to click and 'use' objects (show their popup)
	void DisplayInventory()
	{
		Texture t = null;
		
		float sw = Screen.width;
		float sh = Screen.height;
		
		int totalCellsToDisplay = inventoryObjects.Count;
		for (int i = 0; i < totalCellsToDisplay; i++)
		{
			inventoryItem ii = inventoryObjects[i];
			t = ii.displayTexture;
			int quantity = ii.quantity;

			float totalCellLength = sw - (numCells*width);
			Rect r = new Rect(totalCellLength - 0.5f*(totalCellLength) + (width*i), yPosition*sh, width, height);
			if (GUI.Button(r, t))
			{
				// todo - fill in what to do when user clicks on an item
				if (ii.popup == null)
				{
					ii.popup = (GameObject)Instantiate (ii.item.GetComponent<customGameObject>().popUpInfo, Vector3.zero, Quaternion.identity);
				}
				else
				{
					Destroy(ii.popup);
					ii.popup = null;
				}
			}
			
			Rect r2 = new Rect(totalCellLength - 0.5f*(totalCellLength) + (width*i), yPosition*sh, 0.5f*width, 0.5f*height);
			string s = quantity.ToString();
			GUI.Label(r2, s);
		}
	}
	
	void OnGUI()
	{
		DisplayInventory ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}

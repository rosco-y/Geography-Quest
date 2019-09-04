using UnityEngine;
using System.Collections;

// This component contains a variety of types for the inventoryMgr to use when deciding how to
// accumulate objects.  In this way, objects with different names and textures could add to the same slot
// if they are given the same type
public class customGameObject : MonoBehaviour {
	
	public string displayName;
	public GameObject popUpInfo;
	public enum CustomObjectType
	{
		Invalid = 0,
		Coin = 1,
		Ruby = 2,
		Emerald = 3,
		Diamond = 4,
		flag = 5
	};
	
	public CustomObjectType objectType;
	
	public void validate()
	{
		if (displayName == "")
			displayName = "unnamed_object";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

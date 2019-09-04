using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a specific npc condition that stores a list of data.  Provides a data driven
// way for the decision manager to have access to variables.
public class listData : npcCondition {
	
	public List<GameObject> _listData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override bool eval()
	{
		return true;
	}
}

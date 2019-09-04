using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// an npc condition that associates a floating point variable with the decision manager
// this variable can be passed between scripts like any other component.
public class floatData : npcCondition {

	public float _floatData;

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

using UnityEngine;
using System.Collections;

// a specific npc condition script that checks in an npc has entered a trigger volume
public class condition_onEnter : npcCondition {

	public GameObject trackObj;
	private bool hasEntered = false;

	void OnTriggerEnter(Collider other)
	{
		hasEntered = true;
		trackObj = other.gameObject;
	}
	void OnTriggerExit(Collider other)
	{
		hasEntered = false;
		trackObj = null;
	}

	public override bool eval()
	{
		return hasEntered;
	}
}

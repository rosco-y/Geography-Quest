using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// npc interaction associates conditions with responses.  Used by the 
// decision manager class
[System.Serializable]
public class npcInteraction {
	
	public bool active;
	public npcCondition condition;
	public npcResponse response;

	// Use this for initialization
	void Start () {
	}
	
	public void init()
	{
		if (condition != null)
		{
			if (response != null)
			{
				condition.responseAssociation = response;
				response.conditionAssociation = condition;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public bool eval()
	{
		bool rval = false;
		
		// if the interaction is active, then check the condition
		// if the condition is true, then dispatch the response
		if (active == true)
		{
			if (condition != null)
			{
				if (condition.eval() == true)
				{
					if (response != null)
						rval = response.dispatch();
				}
			}
		}
		return rval;
	}
}
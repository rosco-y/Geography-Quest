using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The core npc decision manager system.  Used by AI actors as it evaluates conditions
// and executes responses when those conditions are met
[System.Serializable]
public class npcDecisionMgr{
	
	public List<npcInteraction> interactions;
	
	public void init()
	{
		foreach (npcInteraction e in interactions)
		{
			e.init ();
		}
	}

	public void eval()
	{
		foreach (npcInteraction e in interactions)
		{
			e.eval();
		}
	}
}

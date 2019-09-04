using UnityEngine;
using System.Collections;

// a specific npc response script that will change the state of an npc
[System.Serializable]
public class response_changeState : npcResponse {

	public npcScript.npcState newstate;
	public npcScript npc;

	public override bool dispatch()
	{
		bool rval = false;
		if (npc != null)
		{
			npc.SetState (newstate);
			rval = true;
		}
		return rval;
	}
}

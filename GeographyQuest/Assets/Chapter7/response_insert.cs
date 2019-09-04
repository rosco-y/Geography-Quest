using UnityEngine;
using System.Collections;

// an npc response script that will insert a GameObject reference into a list
// used for tracking what place the racer achieves when he finishes the race
[System.Serializable]
public class response_insert : npcResponse {
	public npcCondition data;
	public override bool dispatch()
	{
		bool rval = false;
		condition_onEnter cOE = (conditionAssociation as condition_onEnter);
		bool bIsPlayer = (cOE).trackObj.CompareTag ("Player");
		bool bIsRacer = (cOE).trackObj.CompareTag ("Character");
		if (bIsPlayer || bIsRacer)
		{
			listData rlist = (data as listData);
   			if (!rlist._listData.Contains(cOE.trackObj))
      			rlist._listData.Add(cOE.trackObj);
			
			// disable the racer on insert
			if (bIsRacer)
			{
				(cOE).trackObj.GetComponent<npcScript>().SetState (npcScript.npcState.pause);
			}
			rval = true;
		}
		return rval;
	}
}
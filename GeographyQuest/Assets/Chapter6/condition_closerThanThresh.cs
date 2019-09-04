using UnityEngine;
using System.Collections;

// npc condition script to detect when npc is closer than user specified thresh
// used by decisionMgr to decide when to invoke a response
[System.Serializable]
public class condition_closerThanThresh : npcCondition {

	public float thresh;
	public GameObject trackObj;
	public GameObject baseObj;
	
	public override bool eval()
	{
		bool rval = false;

		Vector3 vDisp = (this.baseObj.transform.position - trackObj.transform.position);
		float dist = vDisp.magnitude;
		if ( dist < thresh)
			rval = true;

		return rval;
	}
}

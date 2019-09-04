using UnityEngine;
using System.Collections;

// a specific npc condition script that checks in a list is full.  used by
// the npc decision manager script
public class condition_listFull : npcCondition {
	
	public int numEntries;
	public npcCondition data;

	public override bool eval()
	{
		bool rval = false;

		if (data != null)
		{
			int count = (data as listData)._listData.Count;
			if (count >= numEntries)
				rval = true;
		}

		return rval;
	}
}

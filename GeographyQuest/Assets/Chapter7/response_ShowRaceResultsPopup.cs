using UnityEngine;
using System.Collections;

// a specific npc response script that display the appropriate race results popup
[System.Serializable]
public class response_ShowRaceResultsPopup : npcResponse {
	
	public npcCondition data;
	public GameObject player;
	public gameMgr gm;
	public GameObject passPopup;
	public GameObject retryPopup;

	public override bool dispatch()
	{
		bool rval = false;
		bool playerIsFirst = (data as listData)._listData[0] == player;

		if (playerIsFirst)
		{
			missionMgr mm = gm.GetComponent<missionMgr>();
			if (mm)
			{
				Camera.main.GetComponent<GameCam>().LookUp ();
				if (mm.isMissionComplete(2) == true)
				{
					passPopup.SetActive(true);
				}
				else
				{
					retryPopup.SetActive(true);
				}
			}
		}
		return rval;
	}
}

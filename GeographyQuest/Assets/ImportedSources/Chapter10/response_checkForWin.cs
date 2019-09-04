using UnityEngine;
using System.Collections;

// an npc response that will check for winning condition of the race in level 2.
[System.Serializable]
public class response_checkForWin : npcResponse {
	
	public npcCondition data;
	public GameObject player;
	public gameMgr gm;
	public GameObject passPopup;
	public GameObject retryPopup;
	
	// if player is 1st, and player has won the mission, then just finish the mission now
	// as there is no point for waiting until all 3 players have crossed the finish line
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

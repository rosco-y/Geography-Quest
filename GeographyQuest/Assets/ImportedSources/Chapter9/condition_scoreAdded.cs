using UnityEngine;
using System.Collections;

// an npc condition script that adds score to the player's playerData
public class condition_scoreAdded : npcCondition {
	
	public int scoreAdded;
	public npcCondition initialScore;

	public override bool eval()
	{
		int score = 0;
		GameObject playerObj = GameObject.Find ("Player");
		if (playerObj == null)
			playerObj = GameObject.Find ("Player1");
		if (playerObj != null)
		{
			score = playerObj.GetComponent<playerData>().score;
		}
		bool rval = false;

		if (playerObj != null)
		{
			float playerScore = Mathf.Abs (playerObj.GetComponent<playerData>().score - (initialScore as floatData)._floatData);
			if ((int)playerScore >= scoreAdded )
				rval = true;
		}
		return rval;
	}
}

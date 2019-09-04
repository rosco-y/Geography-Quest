using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The missionMgr class handles the logic for processing each mission; checking when they are finished
public class missionMgr : MonoBehaviour {

	public List<mission> missions;
	public List<missionToken> missionTokens = new List<missionToken>();

	// Use this for initialization
	void Start () {
	}

	public void add(missionToken mt)
	{
		bool uniqueToken = true;
		if (missionTokens != null)
		{
			for (int i = 0; i < missionTokens.Count; i++)
			{
				if (missionTokens[i].id == mt.id)
				{
					// duplicate token found, so abort the insert
					uniqueToken = false;
					break;
				}
			}
		}

		// insert if unique
		if (uniqueToken)
		{
			missionTokens.Add (mt);
		}
	}
	
	public bool isMissionComplete(int missionid)
	{
		bool rval = false;
		if (missionid < missions.Count)
		{
			if (missions[missionid].status == mission.missionStatus.MS_Completed)
				rval = true;
		}
		return rval;
	}

	public bool Validate(mission m)
	{
		bool missionComplete = true;
		
		// a mission with zero tokens is always false
		if (m.tokens.Count <= 0)
			missionComplete = false;

		// for all tokens in the mission
		for (int i = 0; i < m.tokens.Count; i++)
		{
			// search for each one in the cache
			// if it is not in the cache, then abort->this mission is not complete yet
			bool tokenFound = false;
			for (int j = 0; j < missionTokens.Count; j++)
			{
				if (missionTokens[j] != null && (m.tokens[i].id == missionTokens[j].id))
				{	
					tokenFound = true;
					break;
				}
			}

			// searched the entire token cache and didn't find the mission token, so mission must be incomplete
			// lets abort now
			if (tokenFound == false)
			{
				missionComplete = false;
				break;
			}
		}
		
		// award points one time if mission is finished.  We won't do this again because the mission's state will change
		// to MS_COMPLETE and we will no longer evaluate it.
		if (missionComplete == true)
		{
			// get the playerData and add score the player's score
			GameObject go = GameObject.Find ("Player");
			if (go == null)
				go = GameObject.Find ("Player1");
			if (go)
			{
				playerData pd = go.GetComponent<playerData>();
				if (pd)
				{
					pd.AddScore (m.points);
				}
			}
		}

		return missionComplete;
	}
	
	void ValidateAll() {

		//
		//	check the inventory for all of these missions
		//	if one is found, instantiate the reward
		//
		for (int i = 0; i < missions.Count; i++)
		{
			mission m = missions[i];
			
			if (m.status == mission.missionStatus.MS_ForceComplete)
			{
				m.InvokeReward ();
				m.status = mission.missionStatus.MS_Invalid;
			}

			if ((m.status != mission.missionStatus.MS_Completed) && (m.status != mission.missionStatus.MS_Invalid))
			{
				bool missionSuccess = Validate(m);
				
				if (missionSuccess == true)
				{
					// mission complete
					m.InvokeReward();
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		ValidateAll ();
	}
}

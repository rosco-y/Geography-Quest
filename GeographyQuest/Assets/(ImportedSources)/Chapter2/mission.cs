using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this simple class contains the logic description of a mission.  this includes the state (encoded in an enum), as well as the set of
// tokens required to complete the mission.  lastly, the mission contains a reference to a reward gameobject; this can be used to point to a prefab
// that gives the user 'some points', or performs some other logic as result of completion.
[System.Serializable]
public class mission {
	
	public enum missionStatus
	{
		MS_Invalid = -1,
		MS_Acquired = 0,
		MS_InProgress = 1,
		MS_Completed = 2,
		MS_ForceComplete = 3
	};

	public bool activated;
	public bool visible;
	public missionStatus status;
	public string displayName;
	public string description;
	public List<missionToken> tokens;	// the set of tokens (ids) which define the requirements for completing this mission
	public int points;
	public GameObject reward;
	
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public void InvokeReward()
	{
	    // if the mission is finished, instantiate the reward/completion callback
		if (reward != null)
			GameObject.Instantiate(reward);
		this.activated = false;
		this.visible = false;
		this.status = missionStatus.MS_Completed;
	}
}

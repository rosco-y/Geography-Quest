using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// the core npc script.  implements a number of states for an npc actor
public class npcScript : MonoBehaviour {
	
	public enum npcState
	{
		invalid = -1,
		none = 0,
		idle = 1,
		patrol = 2,
		turnToPlayer = 3,
		InteractWithPlayer = 4,
		Celebrate = 5,
		Disappointed = 6,
		startPatrol = 7,
		pause = 8
	};

	private npcState state;
	public bool showPath;
	public npcDecisionMgr decisionMgr;
	public splineMgr path;

	// Use this for initialization
	void Start () {
		SetState (npcState.idle);
		if (path != null)
			path.computeDebugPath();
		if (decisionMgr != null)
			decisionMgr.init();
		SetState(npcState.startPatrol);
	}

	public void SetState(npcState newState)
	{
		// things to do when leaving state and entering s
		switch(newState)
		{
			case(npcState.idle):
			{
				break;
			}
			case(npcState.Celebrate):
			{
				break;
			}
			case(npcState.Disappointed):
			{
				path.SetPlaybackMode(splineMgr.ePlaybackMode.paused);
				break;
			}
			case(npcState.InteractWithPlayer):
			{
				break;
			}
			case(npcState.startPatrol):
			{
				if (path != null)
				{
					path.HeadObj = this.gameObject;
					path.SetPlaybackMode(splineMgr.ePlaybackMode.paused);
					path.reset();
				}
				break;
			}
			case(npcState.patrol):
			{
				path.HeadObj = this.gameObject;
				path.SetPlaybackMode(splineMgr.ePlaybackMode.loop);
				break;
			}
			case(npcState.pause):
			{
				path.HeadObj = this.gameObject;
				path.SetPlaybackMode(splineMgr.ePlaybackMode.paused);
				break;
			}
			case(npcState.turnToPlayer):
			{
				path.SetPlaybackMode (splineMgr.ePlaybackMode.none);
				break;
			}
			case(npcState.none):
			{
				break;
			}
			case(npcState.invalid):
			{
				break;
			}
		}
		state = newState;
	}

	// Update is called once per frame
	void Update ()
	{
		float distanceToHero = 999.0f;
		GameObject player = GameObject.Find ("Player");
		if (player)
		{
			Vector3 v = player.transform.position - this.transform.position;
			distanceToHero = v.magnitude;
		}

		// things to do each tick of state
		switch(state)
		{
			case(npcState.idle):
			{
				break;
			}
			case(npcState.Celebrate):
			{
				break;
			}
			case(npcState.Disappointed):
			{
				break;
			}
			case(npcState.InteractWithPlayer):
			{
				break;
			}
			case(npcState.patrol):
			{
				this.transform.LookAt(path.TargetObj.transform.position);
				break;
			}
			case(npcState.turnToPlayer):
			{
				this.transform.LookAt (player.transform.position);
				break;
			}
		}
		
		//DispatchInteractions();
		if (decisionMgr != null)
			decisionMgr.eval ();
		
		// enable or disable lineRenderer for path
		this.GetComponent<LineRenderer>().enabled = showPath;

	}
}

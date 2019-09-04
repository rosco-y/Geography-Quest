using UnityEngine;
using System.Collections;

// a helper class used by missionMgr to update the state of a specific mission (as specified by name in the MissionName variable)
public class missionMgrHelper : MonoBehaviour {
	
	public string MissionName;
	public bool setActivated;
	public bool setVisible;
	private missionMgr _missionMgr;

	// Use this for initialization
	void Start () {

		_missionMgr = GameObject.Find("Game").GetComponent<missionMgr>();
	}
	
	// Update is called once per frame
	void Update () {
		for ( int i = 0 ; i < _missionMgr.missions.Count; i++)
		{
			mission	m = _missionMgr.missions[i];
			if (m.displayName == MissionName)
			{
				m.activated = setActivated;
				m.visible = setVisible;
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetupMissionOne : MonoBehaviour {
	
	const int kNumFlagMounts = 5;
	public List<GameObject> flagPrefabs;
	private List<GameObject> flagPrefabsBackup;
	public List<GameObject> flagInstances;

	public List<GameObject> spawnPoints;
	private List<GameObject> spawnPointsBackup;
	public List<GameObject> activeSpawnPoints;

	public missionMgr missionManager;
	private bool isInitialized = false;

	// Use this for initialization
	void Start () {
		// turn on the start popup
		GameObject g = GameObject.Find ("MainCamera");
		if (g)
			g.GetComponent<PopupMgr>().Level1Start.SetActive(true);
	}
	
	void SetupMission()
	{
		spawnPointsBackup = new List<GameObject>();
		flagPrefabsBackup = new List<GameObject>();

		// store spawnpoints for later reset
		for (int i = 0; i < spawnPoints.Count; i++)
			spawnPointsBackup.Add (spawnPoints[i]);
		
		//	store flag prefabs for later reset
		for (int i = 0; i < flagPrefabs.Count; i++)
			flagPrefabsBackup.Add (flagPrefabs[i]);
		
		//	pick 5 random spawn points
		//	remove from spawn points list
		//	put into spawnpoint list, so we can place random flag instances there
		activeSpawnPoints = new List<GameObject>();
		for (int k = 0; k < kNumFlagMounts; k++)
		{
			int index = Random.Range (0, spawnPoints.Count);
			GameObject flagSpawnPoint = spawnPoints[index];
			spawnPoints.RemoveAt(index);
			activeSpawnPoints.Add (flagSpawnPoint);
		}

		// add a single mission to the missionManager
		if (missionManager)
		{
			mission m = missionManager.missions[0];
			m.activated = true;
			m.visible = true;
			m.status = mission.missionStatus.MS_Acquired;
			m.displayName = "MissionOne";
			m.description = "collect the 5 randomly placed flags";
			m.tokens.Clear();

			// Get the playerData component, and store the flag choices here.  In later levels, we will populate
			// the npc interactions with these choices if this data exists (otherwise we will use a random selection).
			playerData pd = null;
			GameObject p = GameObject.Find ("Player1");
			if (p == null)
				p = GameObject.Find ("Player");
			if (p != null)
			{
				pd = p.GetComponent<playerData>();
				if (pd.flagChoices.Count > 0)
					pd.flagChoices.Clear();
			}
			
			// pick 5 random flags
			// remove from flagPrefabs
			// instantiate
			// place at random locator
			flagInstances = new List<GameObject>();
			for (int k = 0; k < kNumFlagMounts; k++)
			{
				int index = Random.Range (0,flagPrefabs.Count);
				GameObject flagPrefab = flagPrefabs[index];
				flagPrefabs.RemoveAt (index);
				
				Vector3 flagPos = activeSpawnPoints[k].transform.position;
				flagPos.y += 2.0f;
				GameObject flagInstance = (GameObject)Instantiate (flagPrefab, flagPos, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
				flagInstance.SetActive(true);
				
				if (pd != null)
					pd.flagChoices.Add (index);

				// add flag instance to mission
				m.tokens.Add (flagInstance.GetComponent<missionToken>());
			}
		}

		// add another mission to the missionMgr		
		if (missionManager)
		{
			mission m = missionManager.missions[1];
			m.activated = false;
			m.visible = false;
			m.status = mission.missionStatus.MS_Acquired;
			m.displayName = "MissionTwo";
			m.description = "return the flags to the flagstand";

			m.tokens.AddRange (missionManager.missions[0].tokens);
		}
	}
	
	
	public void SetupPlayer()
	{
		GameObject go = GameObject.Find ("Player1");
		if (go)
		{
			go.GetComponent<playerControls>().EnablePlayerRenderer(true);
		}
	}

	// Update is called once per frame
	void Update () {
		
		if (isInitialized == false)
		{
			GameObject go = GameObject.Find ("Game");
			if (go)
			{
				missionManager = go.GetComponent<missionMgr>();
				if (missionManager)
				{
					SetupMission ();
					SetupPlayer();
					isInitialized = true;
				}
			}
		}
	}
}
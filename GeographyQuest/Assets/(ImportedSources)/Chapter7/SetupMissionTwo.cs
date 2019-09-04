using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a helper script that configures the second mission after it is loaded
public class SetupMissionTwo : MonoBehaviour {

	const int kNumFlagMounts = 5;
	public List<GameObject> quizPrefabs;
	private List<GameObject> quizPrefabsBackup;
	public List<GameObject> quizInstances;
	public List<GameObject> spawnPoints;
	private List<GameObject> spawnPointsBackup;
	public List<GameObject> activeSpawnPoints;
	public List<GameObject> quizNpcInstances;
	public List<GameObject> CorrectPopups;
	private int missionId = 2;

	public GameObject npc2;
	public GameObject npc2hat;
	public GameObject npc3;
	public GameObject npc3hat;
	public GameObject player;
	public GameObject playerhat;
	public GameObject RaceStarterObj;
	public GameObject QuizNpc;
	public GameCam camScript;

	// Use this for initialization
	void Start () {

		RaceStarterObj.SetActive(true);
		if (camScript)
		{
			camScript.height = 0.5f;
		}

		//
		//	Instantiate npcs
		//
		spawnPointsBackup = new List<GameObject>();
		quizPrefabsBackup = new List<GameObject>();
		quizNpcInstances = new List<GameObject> ();

		// store spawnpoints for later reset
		for (int i = 0; i < spawnPoints.Count; i++)
			spawnPointsBackup.Add (spawnPoints[i]);
		
		//	store flag prefabs for later reset
		for (int i = 0; i < quizPrefabs.Count; i++)
			quizPrefabsBackup.Add (quizPrefabs[i]);
		
		//	pick 5 random spawn points
		//	remove from spawn points list
		//	put into spawnpoint list, so we can place random flag instances there
		activeSpawnPoints = new List<GameObject>();
		for (int k = 0; k < kNumFlagMounts; k++)
		{
			int index = Random.Range (0, spawnPoints.Count);
			GameObject quizSpawnPoint = spawnPoints[index];
			spawnPoints.RemoveAt(index);
			activeSpawnPoints.Add (quizSpawnPoint);
		}
		
		missionMgr mm = GameObject.Find ("Game").GetComponent<missionMgr>();

		playerData pd = null;
		if (player == null)
		{
			player = GameObject.Find ("Player");
			if (player == null)
				player = GameObject.Find ("Player1");
		}
		pd = player.GetComponent<playerData>();

		// pick 5 random quizCards
		// remove from flagPrefabs
		// instantiate
		// place at random locator
		//quizInstances = new List<GameObject>();
		for (int k = 0; k < kNumFlagMounts; k++)
		{
			int index = Random.Range (0,quizPrefabs.Count);
			
			// if we have flagChoices, use them instead of the random index;
			if (pd != null)
				index = pd.flagChoices[k];

			GameObject quizPrefab = quizPrefabs[index];
			quizPrefabs.RemoveAt (index);

			Vector3 quizPos = activeSpawnPoints[k].transform.position;
			quizPos.y += 2.0f;
			GameObject QuizNpcInstance = (GameObject)Instantiate (QuizNpc, quizPos, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
			QuizNpcInstance.GetComponent<QuizNpcHelper>().SetPrefabReference(CorrectPopups[k]);
			QuizNpcInstance.SetActive(true);
			QuizNpcInstance.GetComponent<QuizNpcHelper>().SetQuizNpcId(k);
			QuizNpcInstance.transform.parent = GameObject.Find ("_level2").transform;

			// attach quizcard to this npc's interaction
			objectInteraction oo = QuizNpc.GetComponent<objectInteraction>();
			if (oo)
				oo.prefab = quizPrefab;

			// add flag instance to mission
			mm.missions[missionId].tokens.Add (CorrectPopups[k].GetComponent<missionToken>());
		}
		
		// activate mission for the second level
		if (mm)
		{
			mm.missionTokens.Clear();
			mm.missions[missionId].status = mission.missionStatus.MS_Acquired;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

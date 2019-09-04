using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a helper script to initialize level3
public class SetupMissionThree : MonoBehaviour {

	const int kNumFlagMounts = 5;
	public List<GameObject> quizPrefabs;
	private List<GameObject> quizPrefabsBackup;
	public List<GameObject> quizInstances;
	public List<GameObject> spawnPoints;
	private List<GameObject> spawnPointsBackup;
	public List<GameObject> activeSpawnPoints;
	public List<GameObject> quizNpcInstances;
	public List<GameObject> CorrectPopups;
	public npcCondition initialScore;

	public GameObject Npc;
	public GameCam camScript;
	
	void Awake() {
		
		// store initial score
		if (initialScore != null)
		{
			GameObject playerObj = GameObject.Find ("Player1");
			if (playerObj == null)
				playerObj = GameObject.Find ("Player");
			if (playerObj)
			{
				(initialScore as floatData)._floatData = (float)playerObj.GetComponent<playerData>().score;
			}
		}
	}
	// Use this for initialization
	void Start () {
		
		playerData pd = null;
		GameObject playerObj = GameObject.Find ("Player1");
		if (playerObj == null)
			playerObj = GameObject.Find ("Player");
		if (playerObj != null)
		{
			pd = playerObj.GetComponent<playerData>();
		}

//		// store initial score
//		if (initialScore != null)
//		{
//			if (go)
//			{
//				(initialScore as floatData)._floatData = (float)go.GetComponent<playerData>().score;
//			}
//		}

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

		// pick 5 random quizCards
		// remove from flagPrefabs
		// instantiate
		// place at random locator
		for (int k = 0; k < kNumFlagMounts; k++)
		{
			int index = Random.Range (0,quizPrefabs.Count);
			if (pd != null)
				index = pd.flagChoices[k];

			GameObject quizPrefab = quizPrefabs[index];
			objectInteraction objInt = Npc.GetComponent<objectInteraction>();
			if (objInt)
				objInt.prefab = quizPrefab;

			quizPrefabs.RemoveAt (index);

			Vector3 quizPos = activeSpawnPoints[k].transform.position;
			quizPos.y += 2.0f;
			GameObject QuizNpcInstance = (GameObject)Instantiate (Npc, quizPos, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
			QuizNpcInstance.SetActive(true);
			int id = quizPrefab.GetComponent<PopupPanel>().id;
			QuizNpcInstance.GetComponent<QuizNpcHelper>().SetQuizNpcId( id );
			QuizNpcInstance.transform.parent = GameObject.Find ("_level3").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// the script that handles all of the various actions that can occur when the user
// clicks on a popup button.  Extend this script as you find it convenient to add
// more ways of interacting with the game when clicking a button.
public class PopupButtonScript : MonoBehaviour {
	
	public enum popupAction
	{
		Invalid = -1,
		LoadLevel1 = 0,
		LoadLevel2 = 1,
		LoadLevel3 = 2,
		ShowGameObject = 3,
		HideGameObject = 4,
		QuitApplication = 5,
		SelfDestruct = 6,
		Instantiate = 7,
		EnableObject = 8,
		DisableObject = 9,
		AddMissionToken = 10,
		CameraLookUp = 11,
		CameraLookPlayer = 12,
		MakeNpcDance = 13,
		AwardPoints = 14,
		LoadLevelMainMenu = 15,
		ClearInventory = 16,
		ResetScore = 17
	};

	// side data class that associates various data with a button click.  as
	// some actions require and int, a string or a GameObject, we wrap all options
	// into this class
	[System.Serializable]
	public class popupData
	{
		public GameObject obj;
		public int id;
		public string name;
	};
	
	// helper class that associates the action (enum) with the side data required
	// to accomplish the action (int, string, gameObj)
	[System.Serializable]
	public class popupResponse
	{
		public popupAction action;
		public popupData data;
	};

	// the list of actions that a buttonClick may need to accomplish
	public List<popupResponse> actions;
	public enum eButtonState
	{
		Invalid = -1,
		Off = 0,
		On = 1
	};

	public Texture On;
	public Texture Off;
	private Material _mat;
	public eButtonState ButtonState;
	private eButtonState prevTickButtonState;
	private GameObject GameObj;
	private gameMgr gm;

	// Use this for initialization
	void Start () {
		_mat = this.GetComponent<Renderer>().material;
		ButtonState = eButtonState.Off;
		prevTickButtonState = eButtonState.Off;

		GameObj = GameObject.Find ("Game");	
		if (GameObj)
		{
			gm = GameObj.GetComponent<gameMgr>();
		}
	}
	
	public void SetPopup(GameObject newPopup)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			popupResponse resp = actions[i];
			if (resp != null)
				if (resp.data.obj != null)
					if (resp.data.obj.tag == "popup_placeholder")
						resp.data.obj = newPopup;
		}
	}

	void Dispatch()
	{
		for (int i = 0; i < actions.Count; i++)
		{
			popupResponse resp = actions[i];
			switch(resp.action)
			{
				case(popupAction.LoadLevel1):
				{
					if (gm)
					{
						gm.ChangeState(gameMgr.eGameState.eGS_Level1);
						//this.gameObject.SetActive(false);//enabled = false;
					}
					break;
				}
				case(popupAction.LoadLevel2):
				{
					if (gm)
					{
						gm.ChangeState(gameMgr.eGameState.eGS_Level2);
						//this.gameObject.SetActive(false);//enabled = false;
					}
					break;
				}
				case(popupAction.LoadLevel3):
				{
					if (gm)
					{
						gm.ChangeState(gameMgr.eGameState.eGS_Level3);
						//this.gameObject.SetActive(false);//enabled = false;
					}
					break;
				}
				case(popupAction.LoadLevelMainMenu):
				{
					if (gm)
					{
						gm.ChangeState (gameMgr.eGameState.eGS_MainMenu);
					}
					break;
				}
				case(popupAction.AwardPoints):
				{
					GameObject player = GameObject.Find ("Player1");
					if (player)
					{
						playerData pd = player.GetComponent<playerData>();
						if (pd)
						{
							pd.AddScore (resp.data.id);
						}
					}
					break;
				}
				case(popupAction.Instantiate):
				{
					if (gm)
					{
						GameObject go = (GameObject)Instantiate (resp.data.obj);
						Vector3 p = go.transform.position;
						Quaternion q = go.transform.rotation;
						go.transform.position = Vector3.zero;
						go.transform.rotation = Quaternion.identity;
						go.transform.parent = GameObject.Find(resp.data.name).transform;
						go.transform.localPosition = p;
						go.transform.localRotation = q;
					}
					break;
				}
				case(popupAction.EnableObject):
				{
					resp.data.obj.SetActive(true);
					break;
				}
				case(popupAction.DisableObject):
				{
					resp.data.obj.SetActive (false);
					break;
				}
				case(popupAction.HideGameObject):
				{
					resp.data.obj.SetActive (false);
					break;
				}
				case(popupAction.ShowGameObject):
				{
					resp.data.obj.SetActive (true);
					break;
				}
				case(popupAction.QuitApplication):
				{
					Application.Quit ();
					break;
				}
				case(popupAction.CameraLookUp):
				{
					Camera.main.gameObject.GetComponent<GameCam>().LookUp();
					break;
				}
				case(popupAction.CameraLookPlayer):
				{
					Camera.main.gameObject.GetComponent<GameCam>().LookPlayer();
					break;
				}
				case(popupAction.SelfDestruct):
				{
					Destroy (this.transform.parent.gameObject);
					break;
				}
				case(popupAction.MakeNpcDance):
				{
					// loop over all QuizNpcHelpers, and find the one with id same as r.data.id
					Object[] QuizNpcHelperObjects = Object.FindObjectsOfType(typeof(QuizNpcHelper));
        			foreach (Object item in QuizNpcHelperObjects)
        			{
						if ((item as QuizNpcHelper).GetQuizNpcId () == resp.data.id)
						{
							(item as QuizNpcHelper).doSuccess = true;
						}
        			}
					break;
				}
				case(popupAction.ClearInventory):
				{
					GameObject player = GameObject.Find ("Player1");
					if (player == null)
						player = GameObject.Find ("Player");
					if (player)
					{
						inventoryMgr im = player.GetComponent<inventoryMgr>();
						if (im)
						{
							im.inventoryObjects.Clear ();
						}
					}
					break;
				}
				case(popupAction.ResetScore):
				{
					GameObject go = GameObject.Find ("setupLevel3");
					if (go)
					{
						floatData initialScore = go.GetComponent<floatData>();
						if (initialScore)
						{
							GameObject player = GameObject.Find ("Player1");
							if (player == null)
								player = GameObject.Find ("Player");
							if (player)
							{
								player.GetComponent<playerData>().score = (int)initialScore._floatData;
							}
						}
					}
					break;
				}

				case(popupAction.AddMissionToken):
				{
					GameObject mm = GameObject.Find ("Game");
					if (mm)
					{
						missionMgr mmgr = mm.GetComponent<missionMgr>();
						if (mmgr)
						{
							missionToken mt = this.GetComponent<missionToken>();
							if (mt != null)
								mmgr.add (mt);
						}
					}
					break;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		prevTickButtonState = ButtonState;
	}
	
	void OnMouseEnter() {
		_mat.SetTexture ("_MainTex", On);
	}
	
	void OnMouseExit() {
		_mat.SetTexture ("_MainTex", Off);
	}
	
	void OnMouseDown() {
		Dispatch();
	}
}

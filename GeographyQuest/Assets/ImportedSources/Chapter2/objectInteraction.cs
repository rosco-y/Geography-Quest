using UnityEngine;
using System.Collections;

// a component that is used by interactive object in dictating how the players interactions are accomplished.  As you add more ways for the player
// to interact with objects, you may extend this class or break this out into individual classes per interactionAction
public class objectInteraction : MonoBehaviour {

	public enum InteractionAction
	{
		Invalid = -1,
		PutInInventory = 0,
		Use = 1,
		AddMissionToken = 2,
		Instantiate = 3,
		CameraLookUp = 4,
		CameraLookPlayer = 5
	};
	public enum InteractionType
	{
		Invalid = -1,
		Unique = 1,
		Accumulate = 2
	};

	// interaction state vars
	public InteractionAction interaction;
	public InteractionType interactionType;
	public GameObject prefab;
	public Texture _tex;

	// Use this for initialization
	void Start () {
	
	}
	
	void handleUse()
	{
		// to implement in later chapter
	}

	public void handleInteraction()
	{
		inventoryMgr iMgr = null;
		GameObject player = GameObject.Find ("Player");
		if (player == null)
			player = GameObject.Find ("Player1");
		if (player)
			iMgr = player.GetComponent<inventoryMgr>();

		if (interaction == InteractionAction.Use)
			this.handleUse ();

		else if (interaction == InteractionAction.PutInInventory)
		{
			if (iMgr)
				iMgr.Add(this.gameObject.GetComponent<interactiveObj>());
		}
		else if (interaction == InteractionAction.Instantiate)
		{
			GameObject go = (GameObject)Instantiate (prefab, Vector3.zero, Quaternion.identity);
			QuizNpcHelper q = this.gameObject.GetComponent<QuizNpcHelper>();
			if (q != null)
			{
				// pass the correct popup panel to this quiz card
				if (q.prefabReference != null)
					go.GetComponent<PopupPanel>().SetCorrectButtonPopup(q.prefabReference);
			}
		}
		else if (interaction == InteractionAction.CameraLookUp)
		{
			Camera.main.gameObject.GetComponent<GameCam>().LookUp();
		}
		else if (interaction == InteractionAction.CameraLookPlayer)
		{
			Camera.main.gameObject.GetComponent<GameCam>().LookPlayer();
		}
		else if (interaction == InteractionAction.AddMissionToken)
		{
			GameObject mm = GameObject.Find ("Game");
			if (mm)
			{
				missionMgr mmgr = mm.GetComponent<missionMgr>();
				if (mmgr)
				{
					mmgr.add (this.GetComponent<missionToken>());
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

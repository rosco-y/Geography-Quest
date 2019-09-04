using UnityEngine;
using System.Collections;

// a script that manages the core game flow.  This script also handles the streaming in and out of level scene files through the 
// LoadLevelAdditive() method.  As you add more levels to your game, you would extend this system.  Don't forget to add the scene files to the
// project as well as to this enum.
public class gameMgr : MonoBehaviour {
	
	public enum eGameState
	{
		eGS_Invalid = -1,
		eGS_MainMenu = 0,
		eGS_Level1 = 1,
		eGS_Level2 = 2,
		eGS_Level3 = 3
	};
	
	public eGameState gameState;
	private eGameState _prevGameState;

	// Use this for initialization
	void Start () {
		gameState = eGameState.eGS_MainMenu;
		_prevGameState = eGameState.eGS_MainMenu;
	}
	
	public void SetState(eGameState gs)
	{
		gameState = gs;
	}

	public void ChangeState(eGameState gs)
	{
		// destroy all potential levels before signalling a reload before
		gameState = gs;
		Destroy (GameObject.Find ("_level1"));
		Destroy (GameObject.Find ("_level2"));
		Destroy (GameObject.Find ("_level3"));

		switch(gameState)
		{
			case(eGameState.eGS_MainMenu):
			{
				break;
			}
			case(eGameState.eGS_Level1):
			{
				Application.LoadLevelAdditive ("LEVEL1");
				break;
			}
			case(eGameState.eGS_Level2):
			{
				Application.LoadLevelAdditive ("LEVEL2");
				break;
			}
			case(eGameState.eGS_Level3):
			{
				Application.LoadLevelAdditive ("LEVEL3");
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

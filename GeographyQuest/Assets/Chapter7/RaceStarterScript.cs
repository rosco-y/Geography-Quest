using UnityEngine;
using System.Collections;

// a helper script that starts the other zombie races in level 2 to travel along
// their spline paths
public class RaceStarterScript : MonoBehaviour {
	
	public float stageTime = 2.0f;
	public int numStates = 4;
	public int currentState = 0;
	public float t;
	public GameObject npcA;
	public GameObject npcB;

	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		
		if (currentState == numStates+1)
		{
			npcA.GetComponent<npcScript>().SetState (npcScript.npcState.patrol);
			npcB.GetComponent<npcScript>().SetState (npcScript.npcState.patrol);
		}

		if (t > stageTime)
		{
			currentState++;
			t-= stageTime;
		}
	}
}

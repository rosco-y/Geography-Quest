using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a script that contains the specific attributes of the player that need to persist between levels.
// This class could be a good candidate for serialization into a save file 
public class playerData : MonoBehaviour {
	
	public int score;
	public gameMgr.eGameState level;
	public List<int> flagChoices; // use this to pass random questions across levels

	public void AddScore(int dScore)
	{
		score += dScore;
	}
	
	public int GetScore()
	{
		return score;
	}

	public void StoreProgress(gameMgr.eGameState lvl)
	{
		level = lvl;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

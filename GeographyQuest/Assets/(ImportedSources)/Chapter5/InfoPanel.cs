using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// a script to display the information field on the main menu popup.
public class InfoPanel : MonoBehaviour {
	
	public string popupName;
	public TextMesh nametxt;
	public List<TextMesh> TextLines;
	public List<string> textNames;

	// Use this for initialization
	void Start () {
		nametxt.text = popupName;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < TextLines.Count; i++)
		{
			TextLines[i].text = textNames[i];
		}
	}
}

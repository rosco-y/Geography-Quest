using UnityEngine;
using System.Collections;

public class TriviaCardScript : MonoBehaviour {
	
	public float cardWidth;
	public float cardHeight;
	private float sw;
	private float sh;
	private GUITexture gt;

	// Use this for initialization
	void Start () {
	
		sw = Screen.width;
		sh = Screen.height;
		gt = GetComponent<GUITexture>();
		
		float x = ( (sw/2.0f) - (cardWidth/2.0f));
		float y = ( (sh/2.0f) - (cardHeight/2.0f));
		float width = cardWidth;
		float height = cardHeight;
		gt.pixelInset = new Rect(x, y, width, height);

	}
	
	void Update(){
	}

	// Update is called once per frame
	void OnGUI() {
	}
}

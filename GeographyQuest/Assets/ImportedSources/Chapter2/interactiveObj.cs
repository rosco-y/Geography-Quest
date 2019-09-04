using UnityEngine;
using System.Collections;

public class interactiveObj : MonoBehaviour {
	
	//motion state vars
	public Vector3 rotAxis;
	public float rotSpeed;
	public bool billboard;

	// private GameObject information
	private customGameObject _gameObjectInfo;
	public objectInteraction OnCloseEnough;
	private bool activated = true;

	// Use this for initialization
	void Start ()
	{
		_gameObjectInfo = this.gameObject.GetComponent<customGameObject>();
		if (_gameObjectInfo)
			_gameObjectInfo.validate();
	}
	
	// Update is called once per frame
	void Update() {
		
		if (billboard == true)
		{
			GameObject player = GameObject.Find ("Player1");
			if (player != null)
			{
				this.transform.LookAt (player.transform.position);
				this.transform.localEulerAngles = new Vector3(0.0f, this.transform.localEulerAngles.y, 0.0f);
			}
		}
		else
			transform.Rotate (rotAxis, rotSpeed * Time.deltaTime);
	}	

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (activated == true)
			{
				// then fire off the appropriate interaction
				if (OnCloseEnough != null)
					OnCloseEnough.handleInteraction();
				activated = false;
			}
		}
	}
}

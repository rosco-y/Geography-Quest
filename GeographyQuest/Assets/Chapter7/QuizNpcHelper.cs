using UnityEngine;
using System.Collections;

// A helper script used to connect a quiz npc to the question that it asks
// the user.  This is required since this association cannot be known until
// after the level is loaded
public class QuizNpcHelper : MonoBehaviour {

	public GameObject prefabReference;
	public Animator _animator;
	public bool doSuccess = false;
	public int _id = -1;
	
	public int GetQuizNpcId()
	{
		return _id;
	}

	public void SetQuizNpcId(int id)
	{
		_id = id;
	}
	public void SetPrefabReference(GameObject prefabRef)
	{
		prefabReference = prefabRef;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_animator)
		{
			_animator.SetBool ("success",doSuccess);
		}
	}
}

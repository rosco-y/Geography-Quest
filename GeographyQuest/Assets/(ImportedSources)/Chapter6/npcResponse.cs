using UnityEngine;
using System.Collections;

// base response class from which other npc responses can be specialized
abstract public class npcResponse : MonoBehaviour{
	
	public npcCondition conditionAssociation;
	abstract public bool dispatch();
}

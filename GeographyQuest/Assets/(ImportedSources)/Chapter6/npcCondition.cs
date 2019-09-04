using UnityEngine;
using System.Collections;

// base npc condition class from which other npc conditions can be specialized
abstract public class npcCondition : MonoBehaviour{

	public npcResponse responseAssociation;
	abstract public bool eval();
}

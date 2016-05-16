using UnityEngine;
using System.Collections;

public class StageBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("stageBase")){
			Globals.stageBase = gameObject;
		}
	}

}

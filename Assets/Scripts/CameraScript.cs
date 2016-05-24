using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	public GameObject stageBase;

	// Update is called once per frame
	void Update () {
		if (player == null || stageBase == null) {
			Globals.win = false;
			Application.LoadLevel("StageEnd");
			return;
		}
		if (Input.GetKeyDown("p")) {
			Globals.paused = Globals.paused ? false : true;
		}

		if (Globals.paused) {
			return;
		}
	}
}

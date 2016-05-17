using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	public GameObject stageBase;

	// Update is called once per frame
	void Update () {
		if (player == null || stageBase == null) {
			Application.LoadLevel("GameOver");
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

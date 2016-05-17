using UnityEngine;
using System.Collections;

public class CameraScriptLevel1 : MonoBehaviour {
	public GameObject player;
	public GameObject stageBase;
	Vector3 camPos;

	// Use this for initialization
	void Start () {
		camPos = new Vector3 ();
		move(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
	
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

		move(player.transform.position.x, transform.position.y, player.transform.position.z);
	}

	void move(float x, float y, float z){
		camPos.Set (x, y, z);
		transform.position = camPos;
	}
}

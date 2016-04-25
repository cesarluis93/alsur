using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public GameObject player;
	Vector3 camPos;

	// Use this for initialization
	void Start () {
		camPos = new Vector3 ();
		move(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			return;
		}
		move(player.transform.position.x, transform.position.y, player.transform.position.z);
	}

	void move(float x, float y, float z){
		camPos.Set (x, y, z);
		transform.position = camPos;
	}
}

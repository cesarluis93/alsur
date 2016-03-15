using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class main : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject bullets;
	private Vector3 player1Position;
	private Vector3 player2Position;
	private double player1Direction;
	private double player2Direction;
	private int player2DirectionZ;
	private int player2DirectionX;
	private int limitX, limitZ;
	private List<GameObject> enemies;
	private List<GameObject> posibleEnemies;

	// Use this for initialization
	void Start () {
		player1Position = new Vector3(0, 0, 0);
		player2Position = new Vector3(0, 0, 0);
		player1Position = player1.transform.position;
		player2Position = player2.transform.position;
		player2DirectionZ = (int) player2.transform.localEulerAngles.z;
		player2DirectionX =  (int) player2.transform.localEulerAngles.x;
		player1Direction = player1.transform.localEulerAngles.y;
		player2Direction = player2.transform.localEulerAngles.y;
		bullets.AddComponent<Rigidbody> ();
		limitX = 50;
		limitZ = 50;
		enemies = new List<GameObject> ();
		posibleEnemies = new List<GameObject> ();
		posibleEnemies.Add (player2);
		createEnemies (posibleEnemies, 5);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W)) {
			player1Direction = player1.transform.localEulerAngles.y;
			//player1Position += 0.1f;
			player1.transform.position += player1.transform.forward * Time.deltaTime * 30;
		}
		if (Input.GetKey(KeyCode.S)) {
			player1.transform.position -= player1.transform.forward * Time.deltaTime * 30;
		}
		if (Input.GetKey(KeyCode.D)) {
			player1.transform.Rotate (0,3,0);
		}
		if (Input.GetKey(KeyCode.A)) {
			player1.transform.Rotate (0,-3,0);
		}
		if (Input.GetKeyDown(KeyCode.Space)){
			Rigidbody clone = fire (player1);
			clone.velocity = clone.transform.TransformDirection (Vector3.forward * 100);
			clone.AddForce(clone.transform.forward * 30);
		}

	}

	public void listenPlayer2(){
		
	}

	public void updatePlayer2(float posX, float posZ, int angleY){
		player2Position.x = posX;
		player2Position.z = posZ;
		player2.transform.position = player2Position;
		player2Direction = angleY;
		player2.transform.eulerAngles = new Vector3 (player2DirectionX, angleY, player2DirectionZ);
	}

	public Rigidbody fire(GameObject player){
		Vector3 pos = player.transform.position;
		Vector3 trans = player.transform.forward * -8;
		pos -= trans;
		Quaternion rotation = player.transform.rotation;
		GameObject bulletClone = bullets;
		Rigidbody clone = (Rigidbody) Instantiate(bulletClone.GetComponent<Rigidbody>(), pos, rotation);
		clone.gameObject.SetActive (true);
		return clone;
	}

	public void createEnemies(List<GameObject> posibleEnemies, int enemiesCount){
		int minIndex = 0;
		int maxIndex = posibleEnemies.Count - 1;
		int randIndex;
		float x, y, z;
		y = 3.0f;
		Vector3 position;
		int cuadrant;
		for (int i = 0; i < enemiesCount; i++) {
			// Select enemy to create
			randIndex = Random.Range (minIndex, maxIndex);
			// Random coordenates
			x = (float) Random.Range(0, limitX - 5);
			z = (float) Random.Range (0, limitZ - 5);
			// Select caudrant
			cuadrant = Random.Range(0, 4);
			switch (cuadrant) {
				case 0:
					// Both positive
					break;
			case 1:
				// z negative
				z = z * -1f;
				break;
			case 2:
				// Both negative
				z = z * -1f;
				x = x * -1f;
				break;
			case 3:
				// x negative
				x = x * -1f;
				break;
			}
			position = new Vector3 (x, y, z);
			// Spawn enemy
			GameObject enemy = (GameObject)Instantiate (posibleEnemies[randIndex], position, Quaternion.identity);
			enemies.Add (enemy);
		}
			
	}
}

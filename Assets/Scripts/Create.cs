using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Create : MonoBehaviour {
	// To create enemies
	public List<GameObject> possibleEnemies;
	public int cantEnemies;
	public float minX, maxX, minZ, maxZ;

	// Use this for initialization
	void Start () {
		createEnemies(possibleEnemies, cantEnemies, minX, maxX, minZ, maxZ);
	}

	public void createEnemies(List<GameObject> enemies, int cant, float minX, float maxX, float minZ, float maxZ){
		float cantEnemies = enemies.Count;
		float max = ((float)cant) / 2;
		if (cantEnemies > 0) {
			for (int i = 0; i < max; i++) {
				int rIndex = (int) Random.Range(0f, cantEnemies);
				GameObject choosenEnemy = enemies [rIndex];
				Vector3 rPosition = new Vector3 (Random.Range (minX, maxX), choosenEnemy.transform.position.y, Random.Range (minZ, maxZ));
				GameObject insEnemy = Instantiate (choosenEnemy, rPosition, choosenEnemy.transform.rotation) as GameObject;
				insEnemy.SetActive (true);
				rIndex = (int) Random.Range(0f, cantEnemies);
				choosenEnemy = enemies [rIndex];
				rPosition = new Vector3 (Random.Range (-minX, -maxX), choosenEnemy.transform.position.y, Random.Range (-minZ, -maxZ));
				insEnemy = Instantiate (choosenEnemy, rPosition, choosenEnemy.transform.rotation) as GameObject;
				insEnemy.SetActive (true);
			}
		}
	}
}

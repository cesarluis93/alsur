using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Create : MonoBehaviour {
	// To create enemies
	public List<GameObject> possibleEnemies;
	public GameObject boss;
	public float minX, maxX, minZ, maxZ;
	public int waves;
	public int enemiesPerWave;
	// Use this for initialization
	private float time=0;
	private bool bossa=false;

	// Map stuff
	public string jsonMap;
	public List<GameObject> floorObjects;
	public List<GameObject> enviormentObjects;
	public GameObject boundry;

	void Start () {
		createMap (jsonMap, floorObjects, enviormentObjects);
		Globals.enemiesLeft = enemiesPerWave;
		createEnemies(possibleEnemies, enemiesPerWave, minX, maxX, minZ, maxZ);
	}

	void Update(){
		time += Time.deltaTime;
		if (Globals.enemiesLeft == 0 && Globals.waveCount <= waves) {
			createEnemies (possibleEnemies, enemiesPerWave, minX, maxX, minZ, maxZ);
			Globals.enemiesLeft = enemiesPerWave;
			time = 0;
			Globals.waveCount++;
		} 
		else if (!bossa && Globals.waveCount > waves && Globals.enemiesLeft == 0) {
			bossa = true;
			Globals.enemiesLeft++;
			createBoss (boss, minX, maxX, minZ, maxZ);
		}
		else if (bossa && Globals.waveCount > waves && Globals.enemiesLeft == 0) {
			bossa = true;
			Globals.win = true;
		}
	}

	public void createMap(string mapInfoPath, List<GameObject> floor, List<GameObject> enviorment) {
		// Represent JSON in object
		MapInfo mapInfo = MapInfo.createFromJSON (mapInfoPath);

		// With the info create map
		// Get floor objects information
		List<Vector3> sizes = new List<Vector3>();
		for (int i = 0; i < floor.Count; i++) {
			sizes.Add (floor [i].GetComponent<Renderer> ().bounds.size);
		}

		// Get floor objects information
		List<Vector3> sizesEnv = new List<Vector3>();
		for (int i = 0; i < enviorment.Count; i++) {
			sizesEnv.Add (enviorment [i].GetComponent<Renderer> ().bounds.size);
		}	

		// Find max width and height to center stage
		int[][] floorDesc = mapInfo.getFloor();
		float maxWidth = 0;
		float maxHeight = 0;
		int maxColumns = 0;
		int floorIndex;

		// Max width
		for (int i = 0; i < floorDesc.Length; i++){
			int[] floorRow = floorDesc [i];
			float tempWidth = 0;
			int totColumns = floorRow.Length;
			// Keep count of columns to use in loop for height
			if (totColumns > maxColumns) {
				maxColumns = totColumns;
			}
			for (int j = 0; j < totColumns; j++) {
				floorIndex = floorRow [j];
				if (floorIndex >= 0 && floorIndex < sizes.Count) {
					tempWidth += sizes [floorIndex].x;
				}
			}
			// If bigger, it is a max
			if (tempWidth > maxWidth) {
				maxWidth = tempWidth;
			}
		}

		// Max height
		for (int i = 0; i < maxColumns; i++){
			float tempHeight = 0;
			for (int j = 0; j < floorDesc.Length; j++) {
				int[] floorRow = floorDesc [j];
				if (i < floorRow.Length) {
					floorIndex = floorDesc[j] [i];
					if (floorIndex >= 0 && floorIndex < sizes.Count) {
						tempHeight += sizes [floorIndex].z;
					}
				}
			}
			// If bigger, it is a max
			if (tempHeight > maxHeight) {
				maxHeight = tempHeight;
			}
		}

		//With max height and width find offset to center stage
		float offsetX = maxWidth / 2f;
		float offsetZ = maxHeight / 2f;

		float actualWidth = 0f;
		float actualHeight = 0f;
		float lastHeight = 0f;

		// Coordinates to start adding to place the floor
		float startX = -offsetX;
		float startZ = offsetZ;

		// Set the boudries
		// Top
		GameObject topB = Instantiate (
			boundry, new Vector3(0f, boundry.transform.position.y, offsetZ), boundry.transform.rotation
		) as GameObject;
		topB.transform.localScale = new Vector3 (maxWidth, 30f, 1f);
		topB.SetActive (true);
		// Right
		GameObject rightB = Instantiate (
			boundry, new Vector3(offsetX, boundry.transform.position.y, 0f), boundry.transform.rotation
		) as GameObject;
		rightB.transform.localScale = new Vector3 (1f, 30f, maxHeight);
		rightB.SetActive (true);
		// Left
		GameObject leftB = Instantiate (
			boundry, new Vector3(-offsetX, boundry.transform.position.y, 0f), boundry.transform.rotation
		) as GameObject;
		leftB.transform.localScale = new Vector3 (1f, 30f, maxHeight);
		leftB.SetActive (true);
		// Bottom
		GameObject bottomB = Instantiate (
			boundry, new Vector3(0f, boundry.transform.position.y, -offsetZ), boundry.transform.rotation
		) as GameObject;
		bottomB.transform.localScale = new Vector3 (maxWidth, 30f, 1f);
		bottomB.SetActive (true);

		for (int i = 0; i < floorDesc.Length; i++) {
			actualWidth = 0f;
			for (int j = 0; j < floorDesc [i].Length; j++) {
				floorIndex = floorDesc[i][j];
				if (floorIndex >= 0 && floorIndex < floor.Count) {
					GameObject floorElement = floor [floorIndex];
					GameObject floorInst = Instantiate (
						floorElement, new Vector3(startX + actualWidth, floorElement.transform.position.y, startZ + actualHeight), floorElement.transform.rotation
					) as GameObject;
					floorInst.SetActive (true);
					actualWidth += sizes [floorIndex].x;
					lastHeight = sizes [floorIndex].z;
				}
			}
			actualHeight -= lastHeight;
		}

		// Now do the enviorment
		int envIndex = 0;
		actualHeight = 0f;
		lastHeight = 0f;
		int [][] envDesc = mapInfo.getEnviorment();
		for (int i = 0; i < envDesc.Length; i++){
			actualWidth = 0f;
			for (int j = 0; j < envDesc[i].Length; j++){
				envIndex = envDesc[i][j];
				if (envIndex >= 0 && envIndex < enviorment.Count) {
					GameObject envElement = enviorment [envIndex];
					GameObject envInst = Instantiate (
						                     envElement, new Vector3 (startX + actualWidth, envElement.transform.position.y, startZ + actualHeight), envElement.transform.rotation
					                     ) as GameObject;
					envInst.SetActive (true);
					actualWidth += 1f;
					lastHeight = 1f;
				} else {
					lastHeight = 1f;
					actualWidth += 1f;
				}
			}
			actualHeight -= lastHeight;
		}
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

	public void createBoss(GameObject boss, float minX, float maxX, float minZ, float maxZ){
		GameObject choosenEnemy = boss;
		Vector3 rPosition = new Vector3 (Random.Range (minX, maxX), choosenEnemy.transform.position.y, Random.Range (minZ, maxZ));
		GameObject insEnemy = Instantiate (choosenEnemy, rPosition, choosenEnemy.transform.rotation) as GameObject;
		insEnemy.SetActive (true);
	}
}

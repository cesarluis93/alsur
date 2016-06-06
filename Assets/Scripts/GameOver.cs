using UnityEngine;
using System.Collections;
using System.IO;
public class GameOver : MonoBehaviour {

	public Texture gameOverTexture;
	Texture2D texture = null;
	string stageText="";
	string level = "";
	int nextLevel=0;
	// Use this for initialization
	void Start () {
		Globals.waveCount = 0;
		#if UNITY_WEBPLAYER
		print("Not Going To Read That File!");
		#else
		byte[] fileData = null;
		if(Globals.win){
			if(Globals.level > 0 && Globals.level < 3) {
				fileData = File.ReadAllBytes ("Assets/Images/level"+Globals.level+".jpg");
			}
			else{
				fileData = File.ReadAllBytes ("Assets/Images/winner.jpg");
			}
		}
		else{
			fileData = File.ReadAllBytes ("Assets/Images/game_over.jpg");
		}
		texture = new Texture2D(1024, 768);
		texture.LoadImage(fileData); 
		#endif
		Debug.Log ("gui");
		if (Globals.win) {
			if (Globals.level > 0 && Globals.level < 3) {
				stageText = "Next stage";
				level = "Level " + (Globals.level + 1) + " - Assets";
				nextLevel = Globals.level++;
			} else if (Globals.level == 3) {
				stageText = "Back to main menu";
				level = "MainMenu";
				nextLevel = 1;
			}
		} 
		else {
			stageText = "Try again";
			level = "Level " + (Globals.level) + " - Assets";
			nextLevel = Globals.level;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

		// Try again
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height / 2 + 40, 150, 25),
				stageText
			)
		) {
			Globals.level = nextLevel;
			Globals.win = false;
			Application.LoadLevel(level);
		}

		// Quit
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height /2 + 70, 150, 25),
			"Quit"
			)
		)  {
			Application.LoadLevel("MainMenu");
			Globals.level = 1;
			Globals.win = false;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour {


	public Texture mainMenuTexture;
	Texture2D texture = null;

	// Use this for initialization
	void Start () {
		#if UNITY_WEBPLAYER
		print("Not Going To Read That File!");
		#else
		byte[] fileData = File.ReadAllBytes ("Assets/Images/main_menu.jpg");
		texture = new Texture2D(1024, 768);
		texture.LoadImage(fileData); 
		#endif
		Globals.win = false;
		Globals.level = 1;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

		// New Game
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height / 2 + 40, 150, 25),
			"New Game"
		)
		) {
			Application.LoadLevel("Level 1 - Assets");
			Globals.level = 1;
		}
		// Stage Selection
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height /2 + 70, 150, 25),
			"Stage Selection"
		)
		)  {
			Application.LoadLevel("StageSelector");
		}
		// Quit
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height /2 + 100, 150, 25),
			"Quit"
		)
		)  {
			Application.Quit();
		}
	}
}

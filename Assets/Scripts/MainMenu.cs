using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


	public Texture mainMenuTexture;
	Texture2D texture = null;
	public TextAsset imageAsset;

	// Use this for initialization
	void Start () {
		#if UNITY_WEBPLAYER
		texture = new Texture2D(1024, 768);
		texture.LoadImage(imageAsset.bytes); 
		print("Not Going To Read That File!");
		#else
		byte[] fileData = File.ReadAllBytes ("Assets/Images/main_menu.jpg");
		texture = new Texture2D(1024, 768);
		texture.LoadImage(fileData); 
		#endif
	}

	// Update is called once per frame
	void Update () {
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

		// New Game
		if (GUI.Button(
			new Rect(Screen.width / 2, Screen.height / 2 + 40, 150, 25),
			"New Game"
		)
		) {
			Debug.Log("hola");
			Application.LoadLevel("Level 1");
		}

		// Stage Selection
		if (GUI.Button(
			new Rect(Screen.width / 2, Screen.height /2 + 70, 150, 25),
			"Stage Selection"
		)
		)  {
			Application.LoadLevel("Level 1");
		}

		// Quit
		if (GUI.Button(
			new Rect(Screen.width / 2, Screen.height /2 + 100, 150, 25),
			"Quit"
		)
		)  {
			Application.Quit();
		}
	}
}

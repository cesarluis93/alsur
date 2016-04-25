using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Texture gameOverTexture;
	Texture2D texture = null;

	// Use this for initialization
	void Start () {
		byte[] fileData = System.IO.File.ReadAllBytes("Assets/Images/game_over.jpg");
		texture = new Texture2D(1024, 768);
		texture.LoadImage(fileData); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

		// Try again
		if (GUI.Button(
				new Rect(Screen.width / 2, Screen.height / 2 + 40, 150, 25),
				"Try again"
			)
		) {
			Application.LoadLevel("Level 1");
		}

		// Quit
		if (GUI.Button(
			new Rect(Screen.width / 2, Screen.height /2 + 70, 150, 25),
			"Quit"
			)
		)  {
			Application.Quit();
		}
	}
}

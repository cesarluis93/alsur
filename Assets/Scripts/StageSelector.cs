using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine;

public class StageSelector : MonoBehaviour {
	public Texture mainMenuTexture;
	Texture2D level1Texture = null;
	Texture2D level2Texture = null;
	Texture2D level3Texture = null;
	Texture2D bgTexture = null;

	void Start(){
		#if UNITY_WEBPLAYER
		print("Not Going To Read That File!");
		#else
		byte[] fileData;
		fileData = File.ReadAllBytes ("Assets/Images/stage_selector.jpg");
		bgTexture = new Texture2D(1024, 768);
		bgTexture.LoadImage(fileData); 

		fileData = File.ReadAllBytes ("Assets/Images/level_1_preview.png");
		level1Texture = new Texture2D(256, 256);
		level1Texture.LoadImage(fileData); 

		fileData = File.ReadAllBytes ("Assets/Images/level_2_preview.png");
		level2Texture = new Texture2D(256, 256);
		level2Texture.LoadImage(fileData); 

		fileData = File.ReadAllBytes ("Assets/Images/level_3_preview.png");
		level3Texture = new Texture2D(256, 256);
		level3Texture.LoadImage(fileData); 
		#endif
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bgTexture);

		float heightPreviews = Screen.height / 2 - 128;
		// Enter Level 1
		if (GUI.Button(
			new Rect(Screen.width / 2 - 394, heightPreviews, 256, 256),
			level1Texture
		)
		) {
			Application.LoadLevel("Level 1 - Assets");
		}

		if (GUI.Button(
			new Rect(Screen.width / 2 - 128, heightPreviews, 256, 256),
			level2Texture
		)
		) {
			Application.LoadLevel("Level 2 - Assets");
		}

		if (GUI.Button(
			new Rect(Screen.width / 2 + 138, heightPreviews, 256, 256),
			level3Texture
		)
		) {
			Application.LoadLevel("Level 3 - Assets");
		}

		// Quit
		if (GUI.Button(
			new Rect(Screen.width / 2 - 75, Screen.height / 2 + 148, 150, 25),
			"Main Menu"
		)
		)  {
			Application.LoadLevel("MainMenu");
		}
	}
}

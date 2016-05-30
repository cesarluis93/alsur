using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	public Text waves;
	public Text enemiesLeft;

	void OnGUI(){
		waves.text = Globals.waveCount.ToString();
		enemiesLeft.text = Globals.enemiesLeft.ToString();
	}
}

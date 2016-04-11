using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float healthPoints = 100;
	public float score = 0;
	bool dead = false;
	private Animator anim;
	private GUIStyle labelStyle;

	void Start(){
		anim = GetComponent<Animator> ();
		Debug.Log("animator null");

		labelStyle = new GUIStyle();
		labelStyle.fontSize = 18;
		labelStyle.normal.textColor = new Color(0.2F, 0.6F, 1.0F, 1.0F);
	}

	bool ApplyDamage(float damage){
		healthPoints -= damage;

		if (healthPoints <= 0) {
			if (!dead) {
				anim.Play ("LOSE", -1, 0f);
				Destroy (this.gameObject, 2f);
				dead = true;
			}
			return false;
		} 
		else {
			anim.Play ("DAMAGED", -1, 0f);
			dead = false;
		}

		return true;
	}

	void AddScore(float points){
		score += points;
	}

	void OnCollisionEnter(Collision other) {
		bool shot = false;

		if (this.gameObject.tag == "Player") {
			shot = other.gameObject.tag == "eBullet";
		} 
		else {
			shot = other.gameObject.tag == "pBullet";
		}

		if(shot) {
			Globals.player.GetComponent<HealthScript>().AddScore(Globals.rangeScore);
			Debug.Log("bullet damage");
			ApplyDamage (Globals.bulletDamage);
			Destroy (other.gameObject);
		} 
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score.ToString(), labelStyle);
		GUI.Label(new Rect(10, 40, 100, 20), "Points: " + healthPoints.ToString(), labelStyle);
		Debug.Log ("Score: " + score);
		Debug.Log ("Health: " + healthPoints);
	}
}
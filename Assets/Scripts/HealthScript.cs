using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float healthPoints = 100;
	public float score = 0;
	bool dead = false;
	private Animator anim=null;
	private GUIStyle labelStyle;
	private Rigidbody rbody;
	void Start(){
		if(gameObject.tag!="stageBase"){
			anim = GetComponent<Animator> ();
		}
		rbody = GetComponent<Rigidbody> ();
		labelStyle = new GUIStyle();
		labelStyle.fontSize = 18;
		labelStyle.normal.textColor = new Color(0.2F, 0.6F, 1.0F, 1.0F);
	}

	bool ApplyDamage(float damage){
		healthPoints -= damage;
		Debug.Log ("Health Point: "+healthPoints);
		if (healthPoints <= 0) {
			if (!dead) {
				if (anim != null) {
					anim.Play ("die1", -1, 0f);
				}
				Destroy (this.gameObject, 2f);
				Globals.enemiesLeft -= 1;
				dead = true;
			}
		} 
		else {
			if (anim != null) {
				anim.Play ("damage", -1, 0f);	
			}
			dead = false;
		}

		return dead;
	}

	void AddScore(float points){
		score += points;
		Debug.Log (gameObject.tag+":"+score);
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
			if (this.gameObject.tag != "Player") {
				Globals.player.GetComponent<HealthScript>().AddScore(Globals.rangeScore);
			}
			Debug.Log("bullet damage "+Globals.bulletDamage);
			Debug.Log("resultado "+ApplyDamage (Globals.bulletDamage));
			rbody.velocity = Vector3.zero;
			rbody.velocity = Vector3.zero;
		}
		if(other.gameObject.tag.Contains("Bullet")){
			Destroy (other.gameObject);	
		}
	}

	void OnGUI() {
		if (this.gameObject.tag == "Player") {
			GUI.Label(new Rect(10, 10, 100, 20), "Enemies Left: " + Globals.enemiesLeft.ToString(), labelStyle);
			GUI.Label(new Rect(10, 40, 100, 20), "Health: " + healthPoints.ToString(), labelStyle);
			GUI.Label(new Rect(10, 70, 100, 20), "Wave: " + Globals.waveCount.ToString(), labelStyle);
			//Debug.Log ("Score: " + score);
			//Debug.Log ("Health: " + healthPoints);		
		}
	}
}
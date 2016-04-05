using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public float healthPoints=100;
	public float score=0;
	bool dead = false;
	// Use this for initialization
	private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		Debug.Log("animator null");
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
			shot=other.gameObject.tag == "eBullet";
		} 
		else {
			shot=other.gameObject.tag == "pBullet";
		}
		if(shot) {
			Globals.player.GetComponent<HealthScript>().AddScore(Globals.rangeScore);
			Debug.Log("bullet damage");
			ApplyDamage (Globals.bulletDamage);
			Destroy (other.gameObject);
		} 
	}
}
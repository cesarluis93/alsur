using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {
	public float velocity=5.0f;
	public float distance=15;
	private Vector3 result;

	void Update (){
		if(distance>0){
			distance-=activate ();
		}
	}	

	float activate(){
		result = transform.position+ transform.up * Time.deltaTime * velocity;
		float dis = Vector3.Distance (transform.position, result);
		transform.position = result;
		return dis;
	}

	void OnCollisionEnter(Collision other){
		GameObject hit = other.gameObject;
		hit.SendMessage ("ApplyDamage", Globals.carDamage);
	}

}

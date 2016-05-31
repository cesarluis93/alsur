using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {
	public float velocity=4.0f;
	public float distance=15;
	private float distanceMoved;
	private Vector3 result;
	private bool activated;
	public GameObject explosion;
	void Start(){
		distanceMoved = 0;
		activated = false;
	}

	void Update (){
		if (Globals.paused) {
			return;
		}

		if (distanceMoved > 0) {
			distanceMoved -= move ();
		} 
		else {
			activated = false;
		}
			
	}	

	float move(){
		result = transform.position+ transform.right* Time.deltaTime * velocity;
		float dis = Vector3.Distance (transform.position, result);
		transform.position = result;
		return dis;
	}

	void Activate(){
		distanceMoved = distance;
		activated = true;
	}

	void OnCollisionEnter(Collision other){
		GameObject hit = other.gameObject;
		if (hit.tag != "Player" && activated && hit.tag!= "item" && hit.tag!="weapon" && hit.tag!="floor" && hit.tag!="treasureA") {
			hit.SendMessage ("ApplyDamage", Globals.carDamage,SendMessageOptions.DontRequireReceiver);
			distanceMoved = 0;
			activated = false;
			GameObject instance=Instantiate(
				explosion,
				this.transform.position,
				Quaternion.identity
			) as GameObject;
			Destroy (instance, 0.5f);
		}
	}

}

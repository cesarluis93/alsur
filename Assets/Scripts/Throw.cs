using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {
	public float force = 200f;
	public GameObject explosion;

	void Start(){
		throwItem ();
	}

	public void throwItem(){
		Vector3 forward = new Vector3 (this.transform.forward.x, this.transform.forward.y + 1, this.transform.forward.z);
		this.GetComponent<Rigidbody> ().AddForce (forward * force);
	}

	void OnCollisionEnter(Collision other){
		GameObject hit = other.gameObject;
		if (hit.tag != "Player"  && hit.tag != "item" && hit.tag != "weapon") {
			Debug.Log (hit.tag);
			hit.SendMessage ("ApplyDamage", Globals.bombDamage, SendMessageOptions.DontRequireReceiver);
			GameObject instance=Instantiate(
				explosion,
				this.transform.position,
				Quaternion.identity
			) as GameObject;
			Destroy (instance, 0.5f);
			Destroy (this.gameObject, 0.5f);
		}
	}
}

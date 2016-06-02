using UnityEngine;
using System.Collections;

public class MeleeAttackScript : MonoBehaviour {
	public float attackRange = 20f;
	private Vector3 forward;
	RaycastHit hit;

	void Update(){
	}

	void Fire(GameObject gameobject){
		Animator anim = gameobject.GetComponent<Animator> ();
		Debug.Log ("fired");
		anim.Play ("ark1", -1, 0f);
		Vector3 emitter = new Vector3 (
			gameobject.transform.position.x,
			gameobject.transform.position.y + 0.5f,
			gameobject.transform.position.z
		);
		if(Physics.Raycast(emitter, gameobject.transform.forward, out hit, attackRange)){
			Debug.Log ("hit");
			hit.transform.SendMessage ("ApplyDamage", Globals.meleeDamage);
			gameobject.SendMessage ("AddScore", Globals.meleeScore);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.pitch = 2.0f;
			audio.Play ();
		}
	}
}

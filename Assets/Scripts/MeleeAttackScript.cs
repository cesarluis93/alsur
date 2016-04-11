using UnityEngine;
using System.Collections;

public class MeleeAttackScript : MonoBehaviour {
	public float attackRange = 10f;
	private Vector3 forward;
	RaycastHit hit;

	void Fire(GameObject gameobject){
		Animator anim = gameobject.GetComponent<Animator> ();
		Debug.Log ("fired");
		anim.Play ("KICK", -1, 0f);
		if(Physics.Raycast(gameobject.transform.position, gameobject.transform.forward, out hit, attackRange)){
			Debug.Log ("hit");
			hit.transform.SendMessage ("ApplyDamage", Globals.meleeDamage);
			hit.transform.SendMessage ("AddScore", Globals.meleeScore);
		}
	}
}

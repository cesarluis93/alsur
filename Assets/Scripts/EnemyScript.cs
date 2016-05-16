using UnityEngine;
using System.Collections;
public class EnemyScript : MonoBehaviour {
	public Animator anim;
	public Rigidbody rbody;
	private float inputH;
	private float inputV;
	public float attackRange;
	public float velocity = 3f;
	public float turnVelocity = 5f;
	public float cooldownTime = 100f;
	private float cooldown;
	public GameObject weapon;
	public GameObject selectedWeapon;
	public GameObject target;
	public GameObject objective;

	// Use this for initialization
	void Start () {
		pickObjective ();
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		selectedWeapon = Instantiate(
			weapon,
			Vector3.zero,
			Quaternion.identity
		) as GameObject;
		selectedWeapon.transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (Globals.player == null || Globals.paused) {
			return;
		}

		if (objective == null) {
			pickObjective ();
			return;
		}
			
		cooldown -= 1;
		RotateToPlayer();
		if (inRange (attackRange)) {
			anim.SetFloat ("Speed", 0);
			Fire ();
		} 
		else {
			Move(0.8f);
		}
	}

	public void pickObjective(){
		if (Globals.player == null || Globals.stageBase == null) {
			/*
			Debug.Log ("No pick");
			if (Globals.player == null) {
				Debug.Log ("Player null");
			}
			if (Globals.stageBase == null) {
				Debug.Log ("Base null");
			}
			*/
			return;
		}
		int which = Random.Range (0, 2);
		if (which == 0) {
			objective = Globals.player;
			Debug.Log ("Pick player");
		} else {
			objective = Globals.stageBase;
			Debug.Log ("Pick base");
		}
	}

	bool inRange(float range) {

		if (objective == null) {
			return false;
		}
		float distance = Vector3.Distance (objective.transform.position,transform.position);
		if (distance <= range) {
			return true;
		} 
		else {
			return false;
		}

	}

	void RotateToPlayer(){
		if (objective == null) {
			return;
		}
		Vector3 dir = objective.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation (dir);
		transform.rotation=rotation;
		anim.SetFloat ("Direction",rotation.y);
	}

	void Move(float velocity){
		anim.SetFloat ("Speed", 0.8f);
		transform.position += transform.forward * Time.deltaTime * velocity;	
	}
		
	void Fire(){
		if (selectedWeapon != null && cooldown <= 0) {
			Debug.Log ("Calling Fire");
			selectedWeapon.SendMessage("Fire", this.gameObject);
			cooldown = cooldownTime;
		}
	}
}

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
	public GameObject selectedWeapon = null;

	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("Player")){
			Globals.player = gameObject;
		}
		Globals.player = this.gameObject;
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Globals.player == null || Globals.paused) {
			return;
		}

		cooldown -= 1;
		RotateToPlayer();
		if (inRange (attackRange)) {
			anim.SetFloat ("inputV", 0);
			Fire ();
		} 
		else {
			Move(0.8f);
		}
	}

	bool inRange(float range) {
		if (Globals.player == null) {
			return false;
		}
		float distance = Vector3.Distance (Globals.player.transform.position,transform.position);
		if (distance <= range) {
			return true;
		} 
		else {
			return false;
		}

	}

	void RotateToPlayer(){
		if (Globals.player == null) {
			return;
		}
		Vector3 dir = Globals.player.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation (dir);
		transform.rotation=rotation;
	}

	void Move(float velocity){
		anim.SetFloat ("inputV", 0.8f);
		transform.position += transform.forward * Time.deltaTime * velocity;	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.CompareTag("weapon")){
			//gameObject.transform.rotation = new Quaternion (0, gameObject.transform.rotation.y, 0, 0);
			if (selectedWeapon != null) {
				selectedWeapon.transform.localPosition = new Vector3 (2f, 0f, 1f);
				selectedWeapon.transform.parent = null;
				selectedWeapon.transform.localScale = new Vector3 (1f, 1f, 1f);
			}
			Debug.Log ("Picked up");
			selectedWeapon = other.gameObject;
			selectedWeapon.transform.parent = this.transform;
			selectedWeapon.transform.localPosition = new Vector3(0.5f, 0, 0);
			selectedWeapon.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		}
	}
		
	void Fire(){
		if (selectedWeapon != null && cooldown <= 0) {
			Debug.Log ("Calling Fire");
			selectedWeapon.SendMessage("Fire", this.gameObject);
			cooldown = cooldownTime;
		}
	}
}

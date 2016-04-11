using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerScript : MonoBehaviour {
	public Animator anim;
	public Rigidbody rbody;
	private float inputH;
	private float inputV;
	public float velocity = 3f;
	public float turnVelocity = 5f;
	public float cooldownTime = 100f;
	private float cooldown;
	public GameObject selectedWeapon = null;
	private Vector3 prevPos;
	private Quaternion prevRot;

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
		cooldown -= 1;
		inputH = CrossPlatformInputManager.GetAxis ("Horizontal");
		inputV = CrossPlatformInputManager.GetAxis ("Vertical");
		Move (inputH, inputV);
		if(CrossPlatformInputManager.GetButton("Fire1")){
			Fire ();
		}
	}

	void Move(float h, float v){
		anim.SetFloat ("inputV", v);
		float moveZ = v * velocity * Time.deltaTime;
		transform.Rotate (0, h * turnVelocity, 0);
		transform.position += transform.forward * Time.deltaTime * velocity * inputV;
	}

	void OnCollisionEnter(Collision other){
		prevPos = transform.position;
		prevRot = transform.rotation;
		gameObject.transform.rotation = new Quaternion (0, gameObject.transform.rotation.y, 0, 0);
		if(other.gameObject.CompareTag("weapon")){
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

	void OnCollisionExit(Collision other){
		transform.rotation = new Quaternion (0, transform.rotation.y, 0, 0);
		rbody.velocity = Vector3.zero;
		transform.rotation = prevRot;
		transform.position = prevPos;
	}

	void Fire(){
		if (selectedWeapon != null && cooldown <= 0) {
			Debug.Log ("Calling Fire");
			selectedWeapon.SendMessage("Fire", this.gameObject);
			cooldown = cooldownTime;
		}
	}
}

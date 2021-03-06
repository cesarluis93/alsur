﻿using UnityEngine;
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
	private GameObject selectedItem;
	private bool grabItem;
	private string pauseButtonText = "Pause";

	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("Player")){
			Globals.player = gameObject;
		}
		Globals.player = this.gameObject;
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		selectedItem = null;
		grabItem = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Globals.paused) {
			return;
		}

		cooldown -= 1;
		inputH = CrossPlatformInputManager.GetAxis ("Horizontal");
		inputV = CrossPlatformInputManager.GetAxis ("Vertical");
		Move (inputH, inputV);
		if (CrossPlatformInputManager.GetButton ("Fire1")) {
			Fire ();
		}
		else if (grabItem && selectedItem != null && CrossPlatformInputManager.GetButton ("Jump")) {
			selectedItem = Instantiate (selectedItem, this.transform.position, this.transform.rotation) as GameObject;
			selectedItem.SetActive (true);
			grabItem = false;
		}
	}

	void Move(float h, float v){
		//Debug.Log(h);
		anim.SetFloat ("inputV", v);
		float moveZ = v * velocity * Time.deltaTime;
		transform.Rotate (0, h * turnVelocity, 0);
		transform.position += transform.forward * Time.deltaTime * velocity * inputV;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("weapon")) {
			if (selectedWeapon != null) {
				selectedWeapon.transform.localPosition = new Vector3 (2f, 0f, 1f);
				selectedWeapon.transform.parent = null;
				selectedWeapon.transform.localScale = new Vector3 (1f, 1f, 1f);
			}
			Debug.Log ("Picked up");
			selectedWeapon = other.gameObject;
			selectedWeapon.transform.parent = this.transform;
			selectedWeapon.transform.localPosition = new Vector3 (0.5f, 0, 0);
			selectedWeapon.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		}
		else if (other.gameObject.CompareTag ("item")) {
			if (selectedItem != null) {
				DestroyObject (selectedItem);
			}
			PutItem linkToScript = (PutItem) other.gameObject.GetComponent("PutItem");
			selectedItem = linkToScript.getItem ();
			grabItem = true;
			Destroy (other.gameObject);
		}
	}

	void Fire(){
		if (selectedWeapon != null && cooldown <= 0) {
			Debug.Log ("Calling Fire");
			selectedWeapon.SendMessage("Fire", this.gameObject);
			cooldown = cooldownTime;
		}
	}

	void OnGUI() {
		// Pause button
		if (GUI.Button(new Rect(Screen.width - 100, 20, 80, 25), pauseButtonText)) {
			pauseButtonText = pauseButtonText.Equals ("Pause") ? "Resume" : "Pause";
			Globals.paused = Globals.paused ? false : true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public GameObject bullet;
	public float bulletForce = 90f;
	public float bulletTime = 10f;
	private bool firstPause;
	private GameObject Temporary_Bullet_Handler;
	private Vector3[] eVelocities;
	private Vector3[] pVelocities;
	private float tempForce; 
	private GameObject[] eBullets;
	private GameObject[] pBullets;

	void Start(){
		firstPause = false;
	}

	void Update(){
		if (Globals.paused && !firstPause) {
			firstPause = true;
			OnPauseGame ();
		}
		if ((!Globals.paused) && firstPause) {
			firstPause = false;
			OnResumeGame ();
		}
	}

	void OnPauseGame(){
		eBullets =  GameObject.FindGameObjectsWithTag("eBullet");
		eVelocities = new Vector3[eBullets.Length];
		StopBullet (eBullets, "enemy");
		pBullets =  GameObject.FindGameObjectsWithTag("pBullet");
		pVelocities = new Vector3[pBullets.Length];
		StopBullet (pBullets, "player");
	}

	void OnResumeGame(){
		if (eBullets.Length > 0) {
			WakeBullet (eBullets, "enemy");
		}
		if (pBullets.Length > 0) {
			WakeBullet (pBullets, "player");
		}
	}

	void StopBullet(GameObject[] bullets, string type){
		for (int i = 0; i < bullets.Length; i++) {
			GameObject bullet = bullets [i];
			if (bullet != null) {
				Vector3 tempVelocity;
				tempVelocity = bullet.GetComponent<Rigidbody> ().velocity;
				if (type == "enemy") {
					eVelocities [i] = tempVelocity;
				} else if (type == "player") {
					pVelocities [i] = tempVelocity;
				}
				bullet.GetComponent<Rigidbody> ().isKinematic = true;
			}
		}
	}

	void WakeBullet(GameObject[] bullets, string type){
		for (int i = 0; i < bullets.Length; i++) {
			GameObject bullet = bullets [i];
			if (bullet != null) {
				bullet.GetComponent<Rigidbody> ().isKinematic = false;
				Vector3 tempVelocity = new Vector3();
				if (type == "enemy") {
					tempVelocity = eVelocities [i];
				} else if (type == "player") {
					tempVelocity = pVelocities [i];
				}
				bullet.GetComponent<Rigidbody>().AddForce( tempVelocity, ForceMode.VelocityChange );
			}
		}
	}

	void Fire(GameObject gameobject){
		Animator anim = gameobject.GetComponent<Animator> ();
		Vector3 emitter = new Vector3 (
			gameobject.transform.position.x,
			gameobject.transform.position.y + 0.5f,
			gameobject.transform.position.z
		);
		Quaternion rotation = new Quaternion (gameobject.transform.rotation.x, gameobject.transform.rotation.y, gameobject.transform.rotation.z, gameobject.transform.rotation.w);
		Temporary_Bullet_Handler = Instantiate(
			bullet,
			emitter,
			rotation
		) as GameObject;
		Temporary_Bullet_Handler.transform.rotation=rotation;
		Physics.IgnoreCollision (
			gameobject.GetComponent<Collider>(),
			Temporary_Bullet_Handler.GetComponent<Collider>(),
			true
		);
		//Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
		//This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
		Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 270);

		//Retrieve the Rigidbody component from the instantiated Bullet and control it.
		Rigidbody Temporary_RigidBody;
		Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

		//Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
		Temporary_RigidBody.AddForce((	Temporary_Bullet_Handler.transform.up) * bulletForce);

		//Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.

		Temporary_Bullet_Handler.tag = this.gameObject.tag;
		if(gameobject.tag!="fTurret"){
			anim.Play ("atk_bow",-1,0f);
		}
		if (gameobject.tag == "Player" || gameobject.tag == "fTurret") {
			Temporary_Bullet_Handler.tag = "pBullet";
		} 
		else {
			Temporary_Bullet_Handler.tag = "eBullet";
		}

		Destroy(Temporary_Bullet_Handler, bulletTime);	
	}
}

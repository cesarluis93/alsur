using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {
	public GameObject bullet;
	public float bulletForce=90f;
	public float bulletTime=10f;
	void Fire(GameObject gameobject){
		GameObject Temporary_Bullet_Handler;
		Vector3 emitter = new Vector3 (gameobject.transform.position.x,gameobject.transform.position.y+1,gameobject.transform.position.z);
		Temporary_Bullet_Handler = Instantiate(bullet,emitter,gameobject.transform.rotation) as GameObject;
		Physics.IgnoreCollision (gameobject.GetComponent<Collider>(),Temporary_Bullet_Handler.GetComponent<Collider>(),true);
		//Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
		//This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
		//Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

		//Retrieve the Rigidbody component from the instantiated Bullet and control it.
		Rigidbody Temporary_RigidBody;
		Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
		//Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
		Temporary_RigidBody.AddForce(Temporary_Bullet_Handler.transform.forward * bulletForce);
		//Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
		Temporary_Bullet_Handler.tag=this.gameObject.tag;
		if (gameobject.tag == "Player") {
			Temporary_Bullet_Handler.tag = "pBullet";
		} 
		else {
			Temporary_Bullet_Handler.tag = "eBullet";
		}
		Destroy(Temporary_Bullet_Handler, bulletTime);	
	}
}

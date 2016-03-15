using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	GameObject muchacho;
	private Vector3 muchachoPosition;
	private int lives;
	private double muchachoDirectionY;

	void Start () {
		muchachoPosition = new Vector3(0, 0, 0);
		muchachoPosition = muchacho.transform.position;
		muchachoDirectionY =  muchacho.transform.localEulerAngles.y;
	}
		
	void receive(double x, double y, float angle){
		muchachoPosition.x = (float) x;
		muchachoPosition.y = (float) y;
		muchachoDirectionY = angle;
		muchacho.transform.position = muchachoPosition;
		muchacho.transform.Rotate (0,angle,0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Bullet") {
			Destroy(other.gameObject);
			Debug.Log (other.gameObject.tag);
			Debug.Log("Choque bala");
		}

	}
}

using UnityEngine;
using System.Collections;

public class collider : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag != "Player") {
			Destroy(other.gameObject);
			Debug.Log("Choque");
		}

	}
}

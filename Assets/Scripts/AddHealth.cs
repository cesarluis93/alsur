using UnityEngine;
using System.Collections;

public class AddHealth : MonoBehaviour {
	public string validTag;
	public float addHealth;

	void OnCollisionEnter(Collision other) {
		GameObject hisGO = other.gameObject;
		if (hisGO.tag == validTag) {
			// Send more health to the gameobject
			HealthScript health = hisGO.GetComponent("HealthScript") as HealthScript;
			health.addHealth (addHealth);
			Destroy (this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class DisplayObject : MonoBehaviour {
	public int eje;
	public float howMuch;

	// Update is called once per frame
	void Update () {
		Vector3 affect = new Vector3(0f, 0f, 0f);
		switch (eje) {
		case 1:
			affect = new Vector3 (1f, 0f, 0f);
			break;
		case 2:
			affect = new Vector3 (0f, 1f, 0f);
			break;
		case 3:
			affect = new Vector3 (0f, 0f, 1f);
			break;
		}
		Vector3 rotation = new Vector3 (howMuch, howMuch, howMuch);
		rotation = Vector3.Scale(rotation, affect);
		transform.Rotate (rotation);
	}
}

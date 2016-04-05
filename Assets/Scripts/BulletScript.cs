using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private string shooter;
	string GetShooter (){
		return shooter;
	}

	void SetShooter(string shooter){
		this.shooter = shooter;
	}

}

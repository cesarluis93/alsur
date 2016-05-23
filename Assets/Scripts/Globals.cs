using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	public static float meleeDamage = 30f;
	public static float bulletDamage = 20f;
	public static float meleeScore = 30f;
	public static float rangeScore = 20f;
	public static float carDamage = 100f;
	public static int enemiesLeft = 0;
	public static int waveCount = 0;
	public static GameObject player;
	public static GameObject stageBase;
	public static int level=1;
	public static bool win=false;
	public static bool paused = false;
}

//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BossController : MonoBehaviour
{
	
	public float animSpeed = 1.5f;				
	public float lookSmoother = 3.0f;			
	public bool useCurves = true;				
	public float useCurvesHeight = 0.5f;		
	public float forwardSpeed = 7.0f;
	public float backwardSpeed = 2.0f;
	public float rotateSpeed = 2.0f;
	public float jumpPower = 7.0f; 
	private CapsuleCollider col;
	private Rigidbody rb;
	private Vector3 velocity;
	private float orgColHight;
	private Vector3 orgVectColCenter;
	private Animator anim;						
	private AnimatorStateInfo currentBaseState;			

	public GameObject weapon1;
	public GameObject selectedWeapon1;
	public float attackRange1;

	public GameObject weapon2;
	public GameObject selectedWeapon2;
	public float attackRange2;

	static int idle_cState = Animator.StringToHash("Base Layer.Idle_C");
	static int idle_aState = Animator.StringToHash("Base Layer.Idle_A");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int cute1State = Animator.StringToHash("Base Layer.Cute1");

	public float cooldownTime1 = 100f;
	private float cooldown1;	

	public float cooldownTime2 = 100f;
	private float cooldown2;
	private Vector3 target;

	void Start ()
	{
		Collider collider;
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		orgColHight = col.height;
		orgVectColCenter = col.center;
		gameObject.tag = "Boss";
		selectedWeapon1 = Instantiate(
			weapon1,
			Vector3.zero,
			Quaternion.identity
		) as GameObject;
		selectedWeapon1.transform.localScale = Vector3.zero;
		collider = selectedWeapon1.GetComponent<Collider> ();
		collider.enabled = false;
		selectedWeapon2 = Instantiate(
			weapon2,
			Vector3.zero,
			Quaternion.identity
		) as GameObject;
		selectedWeapon2.transform.localScale = Vector3.zero;
		collider = selectedWeapon2.GetComponent<Collider> ();
		collider.enabled = false;
	}
	
	
	void FixedUpdate ()
	{
		target = Globals.player.transform.position;
		cooldown1 -= 1;
		cooldown2 -= 1;
		float h = 0;				
		float v = 0;
		bool jump = false;
		//Revisar rangos de ataque

		if (inRange (attackRange1)&&cooldown1<=0) {
			v = 0;
			Fire1();
		}
		else if(inRange(attackRange2)&&cooldown2<=0){
			v = 0;
			Fire2();
		}
		else if(inRange (attackRange1)||inRange (attackRange2)){
			v = 0.2f;
		}
		else {
			v=0.8f;
			jump = true;
			cooldown1 = cooldownTime1;
			cooldown2 = cooldownTime2;
		}

		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h); 						
		anim.speed = animSpeed;								
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	
		rb.useGravity = true;

		
		velocity = new Vector3(0, 0, v);
		velocity = transform.TransformDirection(velocity);
		if (v > 0.1) {
			velocity *= forwardSpeed;	
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
		if (jump) {	
			if (currentBaseState.nameHash == locoState){
				if(!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}
		
		
		transform.localPosition += velocity * Time.fixedDeltaTime;
		Vector3 dir = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation (dir);
		transform.rotation=rotation;
		anim.SetFloat("Direction", rotation.y);
		if (currentBaseState.nameHash == locoState){
			if(useCurves){
				resetCollider();
			}
		}
		if(currentBaseState.nameHash == jumpState)
		{
			if(!anim.IsInTransition(0))
			{
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{
							
							resetCollider();
						}
					}
				}
				anim.SetBool("Jump", false);
			}
		}

		
		else if (currentBaseState.nameHash == idle_cState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}

			
			
		}
		else if (currentBaseState.nameHash == idle_aState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
			}
		}
		else if (currentBaseState.nameHash == cute1State)
		{
			
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Cute1", false);
			}
		}

	}

	void resetCollider()
	{
		
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}

	void Fire1(){
		Debug.Log ("Attempting to fire1");
		if (selectedWeapon1 != null && cooldown1 <= 0) {
			Debug.Log ("Calling Fire1");
			selectedWeapon1.SendMessage("Fire", this.gameObject);
			cooldown1 = cooldownTime1;
		}
	}

	void Fire2(){
		if (selectedWeapon2 != null && cooldown2 <= 0) {
			Debug.Log ("Calling Fire2");
			selectedWeapon2.SendMessage("Fire", this.gameObject);
			cooldown2 = cooldownTime2;
		}
	}

	bool inRange(float range) {

		if (target == null) {
			return false;
		}
		float distance = Vector3.Distance (target,transform.position);
		if (distance <= range) {
			return true;
		} 
		else {
			return false;
		}

	}
}

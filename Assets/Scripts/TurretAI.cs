using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class TurretAI : MonoBehaviour {
	public GameObject gun;
	private float cooldown;
	public float cooldownTime = 80f;

    public enum AiStates{NEAREST, FURTHEST};
 
    public AiStates aiState = AiStates.NEAREST;
 
    TrackingSystem m_tracker;
    // ShootingSystem m_shooter;
    RangeChecker   m_range;
 
    // Use this for initialization
    void Start () {
        m_tracker =  GetComponent<TrackingSystem>();
        // m_shooter =  GetComponent<ShootingSystem>();
        m_range   =  GetComponent<RangeChecker>();
    }
 
    // Update is called once per frame
    void Update () {
        if(!m_tracker || !m_range)
            return;
		cooldown -= 1;
        switch(aiState)
        {
        case AiStates.NEAREST:
            TargetNearest();
            break;
        case AiStates.FURTHEST:
            TargetFurthest();
            break;
        }
    }
 
    void TargetNearest()
    {
        List<GameObject> validTargets = m_range.GetValidTargets();
 
        GameObject curTarget = null;
        float closestDist = 0.0f;
 
        for(int i = 0; i < validTargets.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, validTargets[i].transform.position);
 
            if(!curTarget || dist < closestDist)
            {
                curTarget = validTargets[i];
                closestDist = dist;
            }
        }
 
        m_tracker.SetTarget(curTarget);
		if (curTarget != null) {
			Fire ();
		}
        // m_shooter.SetTarget(curTarget);
    }
 
    void TargetFurthest()
    {
        List<GameObject> validTargets = m_range.GetValidTargets();
     
        GameObject curTarget = null;
        float furthestDist = 0.0f;
     
        for(int i = 0; i < validTargets.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, validTargets[i].transform.position);
         
            if(!curTarget || dist > furthestDist)
            {
                curTarget = validTargets[i];
                furthestDist = dist;
            }
        }
     
        m_tracker.SetTarget(curTarget);
        // m_shooter.SetTarget(curTarget);
    }

	void Fire() {
		if (gun != null && cooldown <= 0) {
			Debug.Log ("Calling Fire Turret");
			gun.SendMessage("Fire", this.gameObject);
			cooldown = cooldownTime;
		}
	}
}
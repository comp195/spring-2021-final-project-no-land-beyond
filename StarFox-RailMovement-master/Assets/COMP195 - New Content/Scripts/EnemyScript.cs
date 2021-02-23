using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public GameObject player;
	public Transform target; 
 
 	public float maximumLookDistance = 30;
 	public float maximumAttackDistance = 10;
 	public float minimumDistanceFromPlayer = 2;
 
 	public float rotationDamping = 2;
 
 	public float shotInterval = 0.5F;
 
 	private float shotTime = 0;
 
    // Start is called before the first frame update
    void Start()
    {
       	target = player.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
 
		if(distance <= maximumLookDistance)
		{
			LookAtTarget();
		}   
    }

	void LookAtTarget()
	{
		Vector3 dir = target.position - transform.position;
		//dir.y = 0;
		Quaternion rotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}
}

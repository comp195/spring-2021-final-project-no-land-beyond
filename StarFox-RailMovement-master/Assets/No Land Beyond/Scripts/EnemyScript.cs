using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public Transform target; 
 
 	public float maximumLookDistance = 30;
 	public float maximumAttackDistance = 10;
 	public float minimumDistanceFromPlayer = 2;
 
 	public float rotationDamping = 2;

 	public float enemySpeed;
 	public float turn_rate;

 	private float time_to_turn = 0;
 	private float moveX = 1.0F;
 	private float moveY = 1.0F;

 	private int turn_count = 0;
    public int health = 5;




 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); 
		if(distance <= maximumLookDistance)
		{
			LookAtTarget();
		}

		if(Time.time >= time_to_turn)
		{
			time_to_turn = Time.time + 1 / turn_rate;
			moveX = Mathf.Pow(-1, turn_count) * Mathf.Clamp01(Random.value);
			moveY = Mathf.Pow(-1, turn_count) * Mathf.Clamp01(Random.value);
			turn_count++;
		}   
		//LocalMove(moveX, moveY, enemySpeed);
    }

	void LookAtTarget()
	{
		Vector3 dir = target.position - transform.position;
		//dir.y = 0;
		Quaternion rotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}

	void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
    	Projectile bullet = other.gameObject.GetComponent<Projectile>(); 
        if(bullet != null && bullet.fromPlayer){
            health--;
            if(health == 0){
                Die();
            } 	
        }

        //TODO: add additional triggerbox for a win gate

        else if (other.gameObject.CompareTag("Player")){
            Win();
        }
    }

    private void Die(){
        //Death Event
        Destroy(this.gameObject);
        PlayerMovement player = FindObjectsOfType<PlayerMovement>()[0];
        if(player != null){
            player.IncrementKills();
    }
}

    void Win(){
        //Win Event
    }
}

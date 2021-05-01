using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
 
 	public float maximumLookDistance;
 	public float maximumAttackDistance;
 	public float minimumDistanceFromPlayer;
    public CinemachineDollyCart dolly;
 
 	public float rotationDamping;

 	public float enemySpeed;
 	public float turn_rate;

 	private float time_to_turn;
 	private float moveX = 1.0F;
 	private float moveY = 1.0F;

 	private int turn_count = 0;
    public int health;
    public SoundFX sounds;

    public ParticleSystem kaboom;

    // Update is called once per frame
    void Update()
    {

		if(Time.time >= time_to_turn)
		{
			time_to_turn = Time.time + 1 / turn_rate;
			moveX = Mathf.Pow(-1, turn_count) * Mathf.Clamp01(Random.value);
			moveY = Mathf.Pow(-1, turn_count) * Mathf.Clamp01(Random.value);
			turn_count++;
		}   
		transform.parent.localPosition += new Vector3(1, 0, 0) * enemySpeed * Time.deltaTime;
    }

	void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
    	/*Projectile bullet = other.gameObject.GetComponent<Projectile>(); 
        if(bullet != null && bullet.fromPlayer){
            health--;
            if(health == 0){
                Die();
            } 	
        }

        //TODO: add additional triggerbox for a win gate

        else if (other.gameObject.CompareTag("Player")){
            Win();
        }*/
    }

    private void Die(){
        //Death Event
        /*PlayerMovement player = FindObjectsOfType<PlayerMovement>()[0];
        if(player != null){
            player.IncrementKills();
            sounds.Kill(true);
            StartCoroutine("Explode");
        Destroy(this.gameObject);*/
    }
}



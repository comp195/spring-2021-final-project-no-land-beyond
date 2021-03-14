using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemy;
	private float spawnTime = 0;
	public float spawnInterval = 5;
	public float spawnDist = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - spawnTime) > spawnInterval)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy(){
    	spawnTime = Time.time;
    	GameObject spawned = Instantiate(enemy, transform.position + spawnDist*Vector3.forward, Quaternion.identity, transform.parent);
    	spawned.GetComponent<EnemyScript>().target = transform.parent.GetChild(0);
    }
}

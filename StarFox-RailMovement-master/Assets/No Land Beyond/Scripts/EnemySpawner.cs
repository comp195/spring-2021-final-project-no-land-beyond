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
            //SpawnEnemy();
        }
    }


}

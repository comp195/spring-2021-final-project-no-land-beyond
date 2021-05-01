using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text killsText; //displays enemies shot
    public float timePassed; //gives player two minutes
    public Text timerText; // used for showing time

    public float score;
    public int health;
    private int enemiesShot;

    public ProgressBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.BarValue = health;
        timePassed = 0.0f;
        enemiesShot = 0;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.BarValue = health;
    }

    public void TakeDamage(){
        if(health > 0)
            {
                health-=5;
            }
        else
            Die();
    }

    public void Die(){
        StartCoroutine("WaitForDie");
        //Time.timeScale = 0;
        
    }

    IEnumerator WaitForDie(){
        PlayerMovement player = FindObjectsOfType<PlayerMovement>()[0];
        player.Explode();
        yield return new WaitForSeconds(10f);
    }

    public void IncrementKills(){
        enemiesShot++;
    }

    public void ComputeScore(){
        score = enemiesShot;
    }
}

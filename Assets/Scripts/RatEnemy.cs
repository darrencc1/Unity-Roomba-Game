using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{



    private Rigidbody ratRb;
    private GameObject target;
    public float health = 3.0f;
    public float speed;
    public float withinRange;
    // Start is called before the first frame update
    void Start()
    {
        ratRb = GetComponent<Rigidbody>();//access rat rigidbody. 
        target = GameObject.Find("Player");//
        //health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the distance between player(target) and enemy(rat) every frame
        float dist = Vector3.Distance(target.transform.position, transform.position);
        //if player gets too close rat will chase and attack. 
        if(dist <= withinRange)
        {
            //This will make the rat look at player and move to player once player reaches a certain distance from it.
            //target is declaired at Void start, the Player, so the rat position and player position is being calculated. 
            Vector3 lookDirection = (target.transform.position - transform.position).normalized;
            ratRb.AddForce(lookDirection * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //The player can shoot "Bullets" that will hurt the rat if hit. 
        if (other.CompareTag("Bullet"))
        {
            health -= 1;
        }
        if(health <= 0)
        {
            RatDeath();
        }
    }
    private void RatDeath()
    {
        //kills rat
        //own method to allow death animation and other added components for later. 
        Destroy(gameObject);
    }

}

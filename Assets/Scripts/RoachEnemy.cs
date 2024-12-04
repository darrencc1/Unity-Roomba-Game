using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoachEnemy : MonoBehaviour
{
    private GameObject target;
    private Rigidbody roachRb;
    public float speed;
    public float aggroDist;
    public float health = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        roachRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //This works same way as rat, I changed the aggro distance and speed of roach.
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (dist <= aggroDist)
        {
            //calculates vector from roach to player(target) position
            Vector3 lookDirection = (target.transform.position - transform.position).normalized;
            roachRb.AddForce(lookDirection * speed);

        }


    }
    //Will be destroyed if hit by bullet from player. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            //Destroy(gameObject);
            health -= 1;
        }
        if (health <= 0)
        {
            RoachDeath();
        }
    }
    private void RoachDeath()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private Rigidbody collectableRb;
    // Start is called before the first frame update
    void Start()
    {
        collectableRb = GetComponent<Rigidbody>();//Gets Rigidbody of collectable. 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //This will log and say the item is collected
            Debug.Log("Item collected");
            //if player touches the object then it will be destroyed, (It will also be added to player bullets) see PlayerController.cs
            Destroy(gameObject);
        }
    }
}

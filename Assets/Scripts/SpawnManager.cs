using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SpawnManager inherits from MonoBehavior which is the base class for all Unity Scripts, allows the script
//ro be attached to a game Object. 
public class SpawnManager : MonoBehaviour
{
    //This is a list of objects that are collectables I have made. 
    public GameObject[] collectablePrefabs;
    private float spawnLimitXMin = -7.5f;//left and right bounds
    private float spawnLimitXMax = 7.5f;
    private float spawnLimitZMin = -7.5f;//bottom and top bounds
    private float spawnLimitZMax = 7.5f;
    private float spawnposY = 1;

    // Start is called before the first frame update
    void Start()
    {
        //This will spawn a collectable item from the list every 2 to 5 seconds
        float spawnInterval = Random.Range(1.0f, 2.0f);
        //InvokeRepeating is a very useful method that calls a specific method at set intervals
        //So this will start spawning after a 2 second delay at the spawnInterval 
        InvokeRepeating("SpawnRandomCollectable", 1, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Method I made to spawn collectables
    void SpawnRandomCollectable()
    {
        //collectable prefabs are gameobjects I made and put into the list
        //it will take a random index in the list and spawn the object at that index. 
        int collectableIndex = Random.Range(0, collectablePrefabs.Length);
        //This will ensure the objects will be spawned somewhere random within this area
        // and willl spawn no higher than 1 on the y axis. 
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnLimitXMin, spawnLimitXMax),
            spawnposY,
            Random.Range(spawnLimitZMin, spawnLimitZMax)
        );
        //This Instantiate is what will actially create a new Instance of the randomly selected prefab I made
        //the collectablesPrefabs[collectableIndex].transform.rotation is what spawns the collectable
        //at the calculated position and Orintation.! 
        Instantiate(collectablePrefabs[collectableIndex], spawnPos, collectablePrefabs[collectableIndex].transform.rotation);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] ennemyPrefabs;
    public float spawnLimXmax = 7.5f;
    public float spawnLimXmin = -7.5f;
    public float spawnLimZmax = 7.5f;
    public float spawnLimZmin = -7.5f;
    public float spawnPosY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        float spawnInterval = Random.Range(5.0f, 10.0f);
        InvokeRepeating("SpawnRandomEnnemy", 15, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomEnnemy()
    {
        int ennemyIndex = Random.Range(0, ennemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnLimXmax, spawnLimXmin),
            spawnPosY,
            Random.Range(spawnLimZmax, spawnLimZmin));
        Instantiate(ennemyPrefabs[ennemyIndex], spawnPos, ennemyPrefabs[ennemyIndex].transform.rotation);
    }
}

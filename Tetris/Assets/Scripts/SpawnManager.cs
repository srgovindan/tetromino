using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Public variables
    public GameObject[] TetrisBlocksPrefabs;

    //Private variables
    private List<GameObject> _tetrisBlocksBag;
  
    void Start()
    {
        SpawnBlock();
    }

    //This function may be called from other scripts to spawn a tetris block from the spawner location
    public void SpawnBlock()
    {
        //Instantiate the block prefab
        GameObject spawnedBlock = Instantiate<GameObject>(TetrisBlocksPrefabs[Random.Range(0, TetrisBlocksPrefabs.Length)]);
        //Move the block to the spawner location
        spawnedBlock.transform.position = transform.position;
    }
    
}

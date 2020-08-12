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
        //FillBagWithBlocks();
        SpawnBlock();
        //SpawnBlockFromBag();
    }

    //Spawns a tetris block from the spawner location
    public void SpawnBlock()
    {
        //Instantiate the block prefab
        GameObject spawnedBlock = Instantiate(TetrisBlocksPrefabs[Random.Range(0, TetrisBlocksPrefabs.Length)]);
        //Move the block to the spawner location
        spawnedBlock.transform.position = transform.position;
    }

    void FillBagWithBlocks()
    {
        foreach (var block in TetrisBlocksPrefabs)
        {
            _tetrisBlocksBag.Add(block);
        }
    }

    void SpawnBlockFromBag()
    {
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Public variables
    public GameObject[] TetrisBlocksPrefabs;

    //Private variables
    [SerializeField]
    private List<GameObject> _tetrisBlocksBag;
  
    void Start()
    {
        FillBagWithBlocks();
        SpawnBlockFromBag();
    }

    void FillBagWithBlocks()
    {
        foreach (var block in TetrisBlocksPrefabs)
        {
            _tetrisBlocksBag.Add(block);
        }
        
        //Shuffle the order of the blocks
        _tetrisBlocksBag = _tetrisBlocksBag.OrderBy(x => Random.value).ToList();
    }

    //Spawns a block at the spawner location from the bag of pieces
    public void SpawnBlockFromBag()
    {
        if (_tetrisBlocksBag.Count == 0)
        {
            FillBagWithBlocks();
        }
        //Instantiate the block prefab
        GameObject spawnedBlock = Instantiate(_tetrisBlocksBag[0]);
        //Move the block to the spawner location
        spawnedBlock.transform.position = transform.position;
        //Remove the block from the bag
        _tetrisBlocksBag.RemoveAt(0);
    }
    
}

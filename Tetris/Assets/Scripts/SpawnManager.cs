﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// INTENT: The SpawnManager shuffles and deals Tetrominos from a bag of blocks and updates the next block preview UI.
/// USAGE: Place this script on the SpawnManager GameObject.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    //Public variables
    public GameObject[] TetrisBlocksPrefabs;
    public Transform BlockPreviewTransform;

    //Private variables
    [SerializeField]
    private List<GameObject> _tetrisBlocksBag;
    private GameObject _previewBlock;
  
    void Start()
    {
        //Initialize the tetris block bag list
        _tetrisBlocksBag = new List<GameObject>();
        
        //Fill the first bag of blocks
        FillBagWithBlocks();
        SpawnBlockFromBag();
    }

    /// <summary>
    /// Fills a bag with one of each Tetromino for the game to spawn blocks from.
    /// </summary>
    void FillBagWithBlocks()
    {
        //Create a temp list of all the blocks
        List<GameObject> tempBlockList = new List<GameObject>();
        foreach (var block in TetrisBlocksPrefabs)
        {
            tempBlockList.Add(block);
        }
        //Shuffle the order of the blocks in the temp list
        tempBlockList = tempBlockList.OrderBy(x => Random.value).ToList();
        
        //Add the shuffled list of blocks to the tetris bag list
        foreach (var block in tempBlockList)
        {
            _tetrisBlocksBag.Add(block);
        }
    }

    /// <summary>
    /// Spawns a block from the bag and instantiates it at the SpawnManager location,
    /// then updates the next block preview.
    /// </summary>
    public void SpawnBlockFromBag()
    {
        if (_tetrisBlocksBag.Count == 1)
        {
            FillBagWithBlocks();
        }
        //Instantiate the block prefab
        GameObject spawnedBlock = Instantiate(_tetrisBlocksBag[0]);
        //Move the block to the spawner location
        spawnedBlock.transform.position = transform.position;
        //Remove the block from the bag
        _tetrisBlocksBag.RemoveAt(0);
        
        UpdateBlockPreviewUI();
    }
    
    /// <summary>
    /// Updates the preview window with the next block coming up from the bag.
    /// </summary>
    void UpdateBlockPreviewUI()
    {
        //Clear the preview block
        if (_previewBlock != null)
        {
            Destroy(_previewBlock.gameObject);
            _previewBlock = null;
        }

        //Instantiate the block prefab
        _previewBlock = Instantiate(_tetrisBlocksBag[0]);
        //Disable the block script 
        _previewBlock.GetComponent<Block>().enabled = false;
        //Child the preview block to the preview window transform
        _previewBlock.transform.position = BlockPreviewTransform.position - _previewBlock.GetComponent<Block>().rotationPoint;
    }
    
}

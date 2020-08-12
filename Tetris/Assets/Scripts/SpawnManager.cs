using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    //Spawns a block at the spawner location from the bag of pieces
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
        _previewBlock.transform.position = BlockPreviewTransform.position;

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private Transform playerTransform;

    private float zSpawn = 0;
    private float tileLength = 4f;
    private int numTiles2Generate = 5; // generate 3 times earlier than the position of the player

    private List<GameObject> activeTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Add pre initialzied tiles in map to acitveTile list
        foreach (Transform premadeTile in transform)
        {
            if (premadeTile.CompareTag("Tile")){
                activeTiles.Add(premadeTile.gameObject);
                zSpawn += tileLength;
                Debug.Log("Adding premade");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z  > zSpawn - (numTiles2Generate) * tileLength)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            if(playerTransform.position.z > numTiles2Generate * tileLength) DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject newTile = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        newTile.transform.parent = gameObject.transform;
        activeTiles.Add(newTile); // add new tile to active tile list
        Debug.Log("adding tile at " + zSpawn.ToString());

        //tileLength = tilePrefabs[tileIndex].GetComponent<Collider>().bounds.size.z;
        zSpawn += tileLength;

    }

    private void DeleteTile()
    {
        GameObject target = activeTiles[0];
        activeTiles.RemoveAt(0);
        Destroy(target);
    }
}

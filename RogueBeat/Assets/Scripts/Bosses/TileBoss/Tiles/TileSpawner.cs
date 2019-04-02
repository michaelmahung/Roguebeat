using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] float tilePadding = 4.5f;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform spawnLocation;

    public BossTiles[,] SpawnTiles(int arrayFactor)
    {
        if (arrayFactor % 2 == 0)
        {
            Debug.Log("Cant get midpoint of an even number!");
            return null;
        }

        BossTiles[,] Tiles = new BossTiles[arrayFactor, arrayFactor];

        int midPoint = (arrayFactor + 1) / 2;
        float halfPoint = arrayFactor / 2;

        Debug.Log("Midpoint of array is: " + midPoint);

        Debug.Log("Array factor of array is: " + arrayFactor);

        for (int i = 0; i < arrayFactor; i++)
        {

            for (int j = 0; j < arrayFactor; j++)
            {
                if (i == midPoint - 1 && j == midPoint - 1)
                {
                    continue;
                }

                GameObject go = Instantiate(tilePrefab, spawnLocation);
                go.transform.rotation = Quaternion.identity;
                go.transform.localPosition = new Vector3(tilePadding * (i - halfPoint), 0, tilePadding * (j - halfPoint));
                Tiles[i, j] = go.GetComponent<BossTiles>();
                Tiles[i, j].Position = new TilePosition(i, j);
            }

            System.GC.Collect();
        }

        return Tiles;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] float tilePadding = 7f;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform spawnLocation;

    public BossTiles[,] SpawnTiles(int arrayFactor)
    {
        if (arrayFactor % 2 == 0)
        {
            Debug.LogError("Can't create a midpoint from an even number!");
            return null;
        }

        BossTiles[,] Tiles = new BossTiles[arrayFactor, arrayFactor];

        int midPoint = (arrayFactor + 1) / 2;
        float halfPoint = arrayFactor / 2;

        for (int i = 0; i < arrayFactor; i++)
        {

            for (int j = 0; j < arrayFactor; j++)
            {
                GameObject go = Instantiate(tilePrefab, spawnLocation);
                go.transform.rotation = Quaternion.identity;

                if (i == midPoint - 1 && j == midPoint - 1)
                {
                    go.transform.localPosition = new Vector3(tilePadding * (i - halfPoint), -50, tilePadding * (j - halfPoint));
                    Tiles[i, j] = go.GetComponent<BossTiles>();
                    Tiles[i, j].Position = new TilePosition(i, j);
                    continue;
                }

                go.transform.localPosition = new Vector3(tilePadding * (i - halfPoint), 0, tilePadding * (j - halfPoint));
                Tiles[i, j] = go.GetComponent<BossTiles>();
                Tiles[i, j].Position = new TilePosition(i, j);
            }

            System.GC.Collect();
        }

        return Tiles;
    }
}

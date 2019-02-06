using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawning : MonoBehaviour {

    [SerializeField] GameObject prefab;
    [SerializeField] GameObject[,] grid;
    RoomHolder Rooms = new RoomHolder();
    public int GridFactor;
    public int CellOffset;

    void Start () {
        grid = new GameObject[GridFactor, GridFactor];
        Debug.Log(grid.Length);
        PopulateArray(grid);
	}
	
    void PopulateArray(GameObject[,] array)
    {
        for (int i = 0; i < GridFactor; i++)
        {
            for (int j = 0; j < GridFactor; j++)
            {
                array[i, j] = SelectRoomToSpawn(i, j);
            }
        }

        SpawnRooms(array);
    }

    void SpawnRooms(GameObject[,] array)
    {
        for (int i = 0; i < GridFactor; i++)
        {
            for (int j = 0; j < GridFactor; j++)
            {
                Instantiate(prefab, new Vector3(CellOffset * j, 7.5f, CellOffset * i), Quaternion.identity);
                prefab.SetActive(true);
            }
        }
    }

    GameObject SelectRoomToSpawn(int x, int z)
    {
        return prefab;
    }
}

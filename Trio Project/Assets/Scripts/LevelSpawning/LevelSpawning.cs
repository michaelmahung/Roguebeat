using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawning : MonoBehaviour {

    [SerializeField] LevelInfo Level1;
    //[SerializeField] GameObject prefab;
    //[SerializeField] GameObject[,] grid;
    //[SerializeField] RoomInfo[,] AllRooms;
    //[SerializeField] int CellOffset = 51; //This will need to change if the room size changes

    //public int GridFactor;

    private RoomFactory roomFactory;

    void Start () {

        roomFactory = GetComponent<RoomFactory>();
        Level1.CellOffset = 51;
        Level1.GridFactor = 3;
        //AllRooms = new RoomInfo[GridFactor, GridFactor];
        //SetSpawnPositions(AllRooms);
        //GenerateRoomPrefabs(AllRooms);
        //SpawnRooms(AllRooms);
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(2);
        SpawnRoom();
    }

    void SpawnRoom()
    {

    }

    GameObject GeneratePrefab(int x, int z)
    {
        return roomFactory.GrabRandomRoom();
    }

    Vector3 GenerateRoomLocation(int x, int z, LevelInfo level)
    {
        return new Vector3(x * level.CellOffset, 0, z * level.CellOffset);
    }

    #region
    /*void SetSpawnPositions(RoomInfo[,] array)
    {
        for (int i = 0; i < GridFactor; i++)
        {
            for (int j = 0; j < GridFactor; j++)
            {
                array[i, j].RoomLocation = GenerateRoomLocation(i, j);
            }
        }
    }

    void GenerateRoomPrefabs(RoomInfo[,] array)
    {
        for (int i = 0; i < GridFactor; i++)
        {
            for (int j = 0; j < GridFactor; j++)
            {
                array[i, j].RoomPrefab = GeneratePrefab(i, j);
            }
        }
    }

    void SpawnRooms(RoomInfo[,] array)
    {
        for (int i = 0; i < GridFactor; i++)
        {
            for (int j = 0; j < GridFactor; j++)
            {
                Instantiate(array[i, j].RoomPrefab, array[i, j].RoomLocation, Quaternion.identity, transform);
                array[i, j].RoomPrefab.SetActive(true);
            }
        }
    }*/
    #endregion

}

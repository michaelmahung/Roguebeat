using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawning : MonoBehaviour {

    LevelInfo TestLevel;
    RoomFactory roomFactory;
    Vector3 currentLocation;
    Vector3 startRoomLocation;
    Vector3 endRoomLocation;
    int rowSpawnCount;
    bool startRoomSpawned;
    bool endRoomSpawned;

    public enum RoomSpawnTypes { StartingRoom, OpenRoom, BossRoom, LRRoom, LRTRoom, LRBRoom, RandomRoom, DefaultState};
    public RoomSpawnTypes RoomToSpawn;

    void Start () {

        TestLevel = new LevelInfo();
        roomFactory = GetComponent<RoomFactory>();
        TestLevel.RoomOffset = 51;
        TestLevel.GridFactor = 4;

        SetRoomLocation(TestLevel);
        StartCoroutine(StartSpawning());
    }

    //Start at the first row of rooms
    //If there is already a room on my location, move to another location.
    //If not, spawn a room on my location

    void SetRoomLocation(LevelInfo level)
    {
        currentLocation = new Vector3(Random.Range(0, level.GridFactor), 0, level.MinY);
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(2);
        if (!endRoomSpawned)
        {
            SetRoomLocation(TestLevel);
            SpawnRoom(currentLocation, TestLevel);
            StartCoroutine(StartSpawning());
        }else
        {
            StopAllCoroutines();
        }
    }

    RoomSpawnTypes CalculateRoomToSpawn()
    {
        if (!startRoomSpawned)
        {
            startRoomSpawned = true;
            startRoomLocation = currentLocation;
            return RoomSpawnTypes.StartingRoom;
        } else
        {
            return RoomSpawnTypes.OpenRoom;
        }
    }

    void SpawnRoom(Vector3 location, LevelInfo level) 
    {
        if (!TestLevel.IsRoomAlreadySpawned(location))
        {
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn()), location);
            TestLevel.AddToSpawnedRooms(location, room);
            Instantiate(room.RoomType, location * level.RoomOffset, Quaternion.identity, transform);
        } else
        {
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn()), location);
            TestLevel.UpdateRoom(location, room);
            //Destroy()
        }
    }

    GameObject GeneratePrefab()
    {
        return roomFactory.GrabRandomRoom();
    }
}

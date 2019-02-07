/*
 * This class will be used to contain general information about the level being created.
 * What I need to keep track of is the minimum and maximum size of the level, as well as the 
 * distance between each room spawned. 
 */

using UnityEngine;
using System.Collections.Generic;

public class LevelInfo
{
    public int GridFactor; //The amount of cells per row/column
    public int RoomOffset; //The distance between each room cell

    public Vector3 LevelStartPoint { get { return Vector3.zero; } }
    public RoomInformation CurrentRoom;
    public RoomInformation PreviousRoom;

    public int MinX { get { return 0; } }
    public int MinY { get { return 0; } }

    public int MaxX { get { return GridFactor - 1; } }
    public int MaxY { get { return GridFactor - 1; } }

    [SerializeField]
    private Dictionary<Vector3, RoomInformation> SpawnedRooms = new Dictionary<Vector3, RoomInformation>();

    public void AddToSpawnedRooms(Vector3 location, RoomInformation room)
    {
        SpawnedRooms.Add(location, room);
        Debug.Log(SpawnedRooms.Count);
    }

    public bool UpdateRoom(Vector3 location, RoomInformation room)
    {
        try
        {
            SpawnedRooms.Remove(location);
            SpawnedRooms.Add(location, room);
        }catch
        {
            return false;
        }

        return true;
    }

    //Check the spawned room dictionary to see if a room at this location has already been spawned.
    //If it has, return true, otherwise return false.
    public bool IsRoomAlreadySpawned(Vector3 location)
    {
        if (SpawnedRooms.ContainsKey(location))
        {
            return true;
        }

        return false;
    }

    /*When spawning, there are a few key things to keep track of.
     * I need to make sure that each level has an open connection to the final room
     * I need to make sure that rooms do not spawn on top of each other^
     * I need to make sure that rooms do not spawn outside of the grid^
     * I need to make sure that there is a room the player can spawn in
     * I need to make sure that rooms are varied^
     * I need to make sure that room spawning is efficient^
     * I need to be able to track what the previously spawned room was^
     */    
}

public class RoomInformation
{
    public RoomInformation(GameObject type, Vector3 location)
    {
        RoomType = type;
        RoomLocation = location;
    }

    public GameObject RoomType;
    public Vector3 RoomLocation;
}

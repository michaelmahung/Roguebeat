/*
 * This class will be used to contain general information about the level being created.
 *     When spawning, there are a few key things to keep track of.
     * I need to make sure that each level has an open connection to the final room
     * I need to make sure that rooms do not spawn on top of each other^
     * I need to make sure that rooms do not spawn outside of the grid (unless otherwise specified)^
     * I need to make sure that there is a room the player can spawn in^
     * I need to make sure that rooms are varied^
     * I need to make sure that room spawning is efficient^
     * I need to be able to track what the previously spawned room was (in order to link each room together)^
 */

using UnityEngine;
using System.Collections.Generic;

public class LevelFactory
{
    public int GridSize; //The amount of cells per row/column
    public int RoomOffset = 51; //The distance between each room cell

    public Vector3 LevelStartPoint { get { return Vector3.zero; } }
    public RoomInformation CurrentRoom { get; set; }
    public RoomInformation PreviousRoom { get; set; }

    public int MinX { get { return 0; } } //Should always be 0
    public int MinZ { get { return 0; } } //Should always be 0

    public int MaxX { get { return GridSize - 1; } } //The grid factor is 3, make sure we dont go past x = 2
    public int MaxZ { get { return GridSize - 1; } } //""

    int _maxRooms { get; set; }
    public int MaxRooms { get { return _maxRooms - 1; } set { _maxRooms = value; }}//How many rooms maximum do we want to spawn?
    public int CurrentRoomCount { get { return SpawnedRooms.Count; } } //How many rooms have we spawned so far?

    public bool GridBasedSpawns { get; set; } //Should we only spawn within the grid bounds (Min/Max X/Z?
    public bool LinearSpawns { get; set; } //Do we want to spawn rooms linearly?
    public bool CanDoubleBack { get; set; } //Should this level be able to double back when spawning rooms?
    public bool StartRoomSpawned { get; set; } //Has the beginning room been spawned?
    public bool EndRoomSpawned { get; set; } //Has the end/boss room been spawned?
    public bool CanSpawnEnd { get; set; } //Should I spawn an end room?

    //This dictionary will keep track of how many rooms are spawned as well as the location and type of room stored in that room.
    //We want to make sure this is Serialized, because if we are going to save and load level progress, it may be helpful to 
    //reload into a room that has already been spawnd. 
    [SerializeField] private Dictionary<Vector3, RoomInformation> SpawnedRooms = new Dictionary<Vector3, RoomInformation>();

    //Constructors
    #region
    public LevelFactory(int _gridFactor)
    {
        GridSize = _gridFactor;
    }

    public LevelFactory(int _gridFactor, bool _gridBasedSpawns, bool _linearSpawns, bool _canDoubleBack)
    {
        GridSize = _gridFactor;
        GridBasedSpawns = _gridBasedSpawns;
        LinearSpawns = _linearSpawns;
        CanDoubleBack = _canDoubleBack;
    }

    public LevelFactory(int _gridFactor, int _maxRooms, bool _gridBasedSpawns, bool _linearSpawns, bool _canDoubleBack)
    {
        GridSize = _gridFactor;
        GridBasedSpawns = _gridBasedSpawns;
        LinearSpawns = _linearSpawns;
        CanDoubleBack = _canDoubleBack;
        MaxRooms = _maxRooms;
    }
    #endregion

    public void AddToSpawnedRooms(Vector3 location, RoomInformation room)
    {
        SpawnedRooms.Add(location, room);
    }

    //If we need to go back and replace a room that has been previously spawned, we will use this function
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

    public void DeleteRoom(Vector3 location)
    {
        Debug.Log(SpawnedRooms[location].RoomName);
        Object.Destroy(GameObject.Find(SpawnedRooms[location].RoomName));
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
}

    //If this class didn't have a constructor i'd just make it a struct
    //All it's doing is keeping track of where it is, as well as the type/prefab of room it is spawning.
    public class RoomInformation
    {
        public RoomInformation(GameObject type, Vector3 location, string name)
        {
            RoomName = name;
            RoomType = type;
            RoomLocation = location;
        }

        public GameObject RoomType;
        public Vector3 RoomLocation;
        public string RoomName;
    }


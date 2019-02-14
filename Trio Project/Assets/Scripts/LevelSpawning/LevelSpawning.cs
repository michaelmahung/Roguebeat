using System.Collections;
using UnityEngine;

//TODO -- REFACTOR ALL OF THIS
public class LevelSpawning : MonoBehaviour {

    LevelFactory TestLevel;
    RoomFactory roomFactory;
    Vector3 currentLocation;
    Vector3 startRoomLocation;
    Vector3 endRoomLocation;
    int rowSpawnCount;
    //bool levelStartRoomSpawned;
    //bool levelEndRoomSpawned;
    //bool levelCanEndSpawn;

    public enum RoomSpawnTypes { StartingRoom, OpenRoom, BossRoom, LRRoom, LRTRoom, LRBRoom, RandomRoom, DefaultState};
    public RoomSpawnTypes RoomToSpawn;

    Vector3
        North = new Vector3(0, 0, 1),
        East = new Vector3(1, 0, 0),
        West = new Vector3(-1, 0, 0),
        PreviousSpawnLocation,
        PreviousMoveDirection,
        CurrentMoveDirection;

    public delegate void FinishedSpawning();
    public static FinishedSpawning FinishedSpawningRooms;

    void Start () {

        //Constructor for a new level, specifies the grid size, the max amount of rooms, whether it adheres strictly to the bounds of the grid
        //if it should spawn more linear rooms, and whether or not the spawns should double back on each other - in that order
        TestLevel = new LevelFactory(4, 15, true, false, false);

        roomFactory = GetComponent<RoomFactory>();

        /*Spawning should happen in the following order
         * Find an open location to spawn a room in
         * Figure out which room type to spawn
         * Spawn the room
         */

        FindNextRoomLocation(TestLevel);
        StartCoroutine(StartSpawning());
    }

    //Coroutine that will handle spawning rooms slowly - so we can see the level creation process.
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(0.10f);
        if (!TestLevel.EndRoomSpawned)
        {
            FindNextRoomLocation(TestLevel);
            SpawnRoom(currentLocation, TestLevel);
            StartCoroutine(StartSpawning());
        }else
        {
            FinishedSpawningRooms();
            StopAllCoroutines();
        }
    }

    //If we are just starting off, we want to pick a random location in the first row and spawn a starting room.
    //This is important because every room thereafter HAS to be linked to this room in some way.
    //If the starting room has already been spawned, start incrementing our location by 1 and moving.
    void FindNextRoomLocation(LevelFactory level)
    {
        if (!level.StartRoomSpawned)
        {
            currentLocation = new Vector3(Random.Range(0, level.GridSize), 0, level.MinZ);
            PreviousSpawnLocation = currentLocation;
            return;
        }

        //If doubling back is NOT allowed, calculate a move direction and check if there is a room in that direction
        //While there isn't an empty room in our requested direction, keep trying directions.

        if (!level.CanDoubleBack)
        {
            CurrentMoveDirection = CalculateMoveDirection(level);

            while(level.IsRoomAlreadySpawned(currentLocation + CurrentMoveDirection))
            {
                //For as long as we are on top of another rooms location, pick a new location and try to spawn there.
                CurrentMoveDirection = CalculateMoveDirection(level);
            }

            PreviousSpawnLocation = currentLocation;
        }
        if (level.CanDoubleBack)
        {
            //If we are allowed to double back, go back to our previous location and try to find an open space
            currentLocation = PreviousSpawnLocation;
            CurrentMoveDirection = CalculateMoveDirection(level);

            while(level.IsRoomAlreadySpawned(currentLocation + CurrentMoveDirection))
            {
                CurrentMoveDirection = CalculateMoveDirection(level);
            }

            PreviousSpawnLocation = currentLocation;
        }

        currentLocation += CurrentMoveDirection;
    }

    void SpawnLevelRooms()
    {
        while (!TestLevel.EndRoomSpawned)
        {

        }
    }

    //Does as it says, which room should I be spawning?
    RoomSpawnTypes CalculateRoomToSpawn(LevelFactory level)
    {
        //If the starting room hasnt been spawned yet, start by spawning that room
        if (!level.StartRoomSpawned)
        {
            level.StartRoomSpawned = true;
            startRoomLocation = currentLocation;
            return RoomSpawnTypes.StartingRoom;
        }
        //If im in my max row or at my max amount of rooms, spawn a boss room
        else if (level.CanSpawnEnd || level.CurrentRoomCount == level.MaxRooms)
        {
            Debug.Log("Spawning end room");
            level.EndRoomSpawned = true;
            return RoomSpawnTypes.BossRoom;
        }
        //Otherwise, spawn an OpenRoom TODO - Link + Spawn other room types
        else
        {
            return RoomSpawnTypes.OpenRoom;
        }
    }

    void SpawnRoom(Vector3 location, LevelFactory level) 
    {
        //If there isnt already a room spawned at our location
        if (!TestLevel.IsRoomAlreadySpawned(location))
        {
            //Create a new room at our location, name it, and pass it into our room dictionary.
            //Instantiate the room type we generated, and name it accordingly.
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location, "Room" + level.CurrentRoomCount);
            GameObject go = Instantiate(room.RoomType, location * level.RoomOffset, Quaternion.identity, transform);

            if (level.EndRoomSpawned)
            {
                go.name = "End Room";
                return;
            }

            go.name = room.RoomName;
            TestLevel.AddToSpawnedRooms(location, room);
            return;
        }
        else
        //If theres a room on top of ours and the level doesn't allow us to backtrack through old rooms.
        {
            if (!level.CanDoubleBack)
            {
                //Generate a room
                RoomInformation endRoom = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location, "Room" + level.CurrentRoomCount);

                //If we're allowed to spawn an end room
                if (level.EndRoomSpawned)
                {
                    //And were currently on top of our starting room
                    if (currentLocation == startRoomLocation)
                    {
                        //Move one space north and spawn the final room
                        GameObject go = Instantiate(endRoom.RoomType, (location + North) * level.RoomOffset, Quaternion.identity, transform);
                        go.name = "End Room";
                        return;
                    }
                    else
                    {
                        //Otherwise, move north and create a new room at that location
                        //currentLocation += North;
                        GameObject go = Instantiate(endRoom.RoomType, (location + North) * level.RoomOffset, Quaternion.identity, transform);
                        go.name = "End Room";
                        return;
                    }
                }
            }

            //Otherwise, if we can double back and there is a room in our way
            currentLocation = PreviousSpawnLocation;
            Vector3 direction = CalculateMoveDirection(level);
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location + direction, "Room" + level.CurrentRoomCount);
            level.UpdateRoom(location + direction, room);
            GameObject dBgo = Instantiate(room.RoomType, ((location + direction) * level.RoomOffset), Quaternion.identity, transform);

            if (level.EndRoomSpawned)
            {
                dBgo.name = "End Room";
            } else
            {
                dBgo.name = room.RoomName;
            }

            return;
        }
    }

    GameObject GeneratePrefab()
    {
        return roomFactory.GrabRandomRoom();
    }

    Vector3 CalculateMoveDirection(LevelFactory level)
    {
        bool eastSpawned = TestLevel.IsRoomAlreadySpawned(currentLocation + East);
        bool westSpawned = TestLevel.IsRoomAlreadySpawned(currentLocation + West);
        bool northSpawned = TestLevel.IsRoomAlreadySpawned(currentLocation + North);
        
        //TODO - rework direction picking and spawning
         
        //Debug.Log(currentLocation + " " + level.MaxZ + " " + level.MaxX);

        if (level.GridBasedSpawns) //If we only want to spawn inside our grid...
        {
            //If were not in our last row
            if (currentLocation.z != level.MaxZ)
            {
                //If we are at our maximum X value.
                if (currentLocation.x == level.MaxX)
                {
                    //If were at our max X and theres a room to our left, move north.
                    if (westSpawned)
                    {
                        return North;
                    }
                    else
                    {
                        //Otherwise, decide if we will move North or West (66% chance of West).
                        int randNW = Random.Range(0, 5);

                        if (randNW > 0)
                        {
                            if (!level.LinearSpawns)
                            {
                                if (randNW > 3)
                                {
                                    return West;
                                }
                            }
                                return West;
                        }

                        return North;
                    }
                }
                else if (currentLocation.x == level.MinX)
                {
                    //If we are at our minimum X and theres a room to our right, move north
                    if (eastSpawned)
                    {
                        return North;
                    }
                    else
                    {
                        //Otherwise pick a #
                        int randNE = Random.Range(0, 5);

                        if (randNE > 0)
                        {
                            if (!level.LinearSpawns)
                            {
                                if (randNE > 3)
                                    return East;
                            }
                                return East;
                        }

                        return North;
                    }
                }
                else
                {
                    //If we are not at our MaxX or MinX
                    int randNWE = Random.Range(0, 7);

                    if (randNWE >= 0 && randNWE <= 2)
                    {
                        return East;
                    }
                    else if (randNWE >= 3 && randNWE <= 5)
                    {
                        return West;
                    }

                    return North;
                }
            } else
            {
                //If we ARE in our last row
                int rand2 = Random.Range(0, 7);

                if (rand2 >= 0 && rand2 <= 2 && !eastSpawned && currentLocation.x != level.MaxX)
                {
                    return East;
                }
                else if (rand2 >= 3 && rand2 <= 5 && !westSpawned && currentLocation.x != level.MinX)
                {
                    return West;
                }
                else
                {
                    level.CanSpawnEnd = true;
                    Debug.Log("preparing to spawn last room");
                }
            }
        }

        //OTHERWISE IF WE ARE NOT SPAWNING BASED ON A GRID (MEANING WE ARE SPAWNING BASED OFF A ROOM COUNT ONLY)
        else if (!level.GridBasedSpawns)
        {
            //TODO - finish logic for non-grid based spawns
        }

        //If all else fails, move North
        return North;
    }
}

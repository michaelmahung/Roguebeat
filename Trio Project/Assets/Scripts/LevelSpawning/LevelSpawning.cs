using System.Collections;
using UnityEngine;

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
        South = new Vector3(0, 0, -1),
        West = new Vector3(-1, 0, 0),
        PreviousSpawnLocation,
        PreviousMoveDirection,
        CurrentMoveDirection;

    void Start () {

        TestLevel = new LevelFactory(10, 15, true, true, true);
        roomFactory = GetComponent<RoomFactory>();

        SetRoomLocation(TestLevel);
        StartCoroutine(StartSpawning());
    }

    //Coroutine that will handle spawning rooms slowly - so we can see the level creation process.
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(0.10f);
        if (!TestLevel.EndRoomSpawned)
        {
            SetRoomLocation(TestLevel);
            SpawnRoom(currentLocation, TestLevel);
            StartCoroutine(StartSpawning());
        }else
        {
            StopAllCoroutines();
        }
    }

    //If we are just starting off, we want to pick a random location in the first row and spawn a starting room.
    //This is important because every room thereafter HAS to be linked to this room in some way.
    //If the starting room has already been spawned, start incrementing our location by 1 and moving.
    void SetRoomLocation(LevelFactory level)
    {
        if (!level.StartRoomSpawned)
        {
            currentLocation = new Vector3(Random.Range(0, level.GridSize), 0, level.MinZ);
            PreviousSpawnLocation = currentLocation;
            return;
        }

        PreviousSpawnLocation = currentLocation;

        PreviousMoveDirection = CalculateMoveDirection(level);
        currentLocation += PreviousMoveDirection;
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
            //Debug.Log("No Room Here");
            //Create a new room at our location, name it, and pass it into our room dictionary.
            //Instantiate the room type we generated, and name it accordingly.
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location, "Room" + level.CurrentRoomCount);
            TestLevel.AddToSpawnedRooms(location, room);
            GameObject go = Instantiate(room.RoomType, location * level.RoomOffset, Quaternion.identity, transform);
            if (level.EndRoomSpawned)
            {
                go.name = "End Room";
                return;
            }
            go.name = room.RoomName;
            return;
        }
        else
        //If theres a room on top of ours and the level doesn't allow us to backtrack through old rooms.
        {
            //Debug.Log("Theres a room here");
            if (!level.CanDoubleBack)
            {
                //Debug.Log("Cant double back");
                //Generate a room
                RoomInformation endRoom = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location, "Room" + level.CurrentRoomCount);

                //If we're allowed to spawn an end room
                if (level.EndRoomSpawned)
                {
                    //Debug.Log("Looking to spawn end room");
                    //And were not currently on top of our starting room
                    if (currentLocation == startRoomLocation)
                    {
                        //Debug.Log("On top of spawn room");
                        //currentLocation += North;
                        //Delete the room at our current location and replace it with our new room
                        //level.DeleteRoom(currentLocation);
                        //level.UpdateRoom(currentLocation, endRoom);
                        GameObject go = Instantiate(endRoom.RoomType, (location + North) * level.RoomOffset, Quaternion.identity, transform);
                        go.name = "End Room";
                        return;
                    }
                    else
                    {
                        //Debug.Log("not on top of spawn room" + currentLocation + " spawn location " + startRoomLocation);
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
            RoomInformation room = new RoomInformation(roomFactory.GrabRoom(CalculateRoomToSpawn(level)), location, "Room" + level.CurrentRoomCount);
            GameObject dBgo = Instantiate(room.RoomType, ((location + North) * level.RoomOffset), Quaternion.identity, transform);
            if (level.EndRoomSpawned)
            {
                dBgo.name = "End Room";
            }


            level.UpdateRoom(location, room);
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
        bool southSpawned = TestLevel.IsRoomAlreadySpawned(currentLocation + South);
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

    /*
     * 
     * else //Otherwise we'll go off of a MaxRoomCount
        {

            //If we are as far east as we can go and theres a room spawned to the west of us, move north.
            if (currentLocation.x == level.MaxX && westSpawned && currentLocation.z != level.MaxZ)
            {
                return North;
            }
            //If we are as far west as we can go and theres a room spawned to the east of us, move north.
            else if (currentLocation.x == level.MinX && eastSpawned && currentLocation.z != level.MaxZ)
            {
                return North;
            }
            //If im not at my X or Z bounds, move east or west.


            //Need to change this, should check if at Max/Min X - and roll for East/North or West/North accordingly.
            //Weight will by 70% chance East/West, 30% North.
            //**Actually just make a bool that will allow for unconstrained spawns and go off of a total room count.

            else if (currentLocation.z != level.MaxZ)
            {
                int rand = Random.Range(0, 2);

                if (rand == 0 && !eastSpawned)
                {
                    return East;
                }
                else if (rand == 1 && !westSpawned)
                {
                    return West;
                }
            }
            else if (currentLocation.z == level.MaxZ)
            {
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
                    canSpawnEnd = true;
                }
            }
        }
                //Need to change this, should check if at Max/Min X - and roll for East/North or West/North accordingly.
        //Weight will by 70% chance East/West, 30% North.

        else if (currentLocation.z != level.MaxZ)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0 && !eastSpawned)
            {
                return East;
            }
            else if (rand == 1 && !westSpawned)
            {
                return West;
            }
        }
        else if (currentLocation.z == level.MaxZ)
        {
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
                canSpawnEnd = true;
            }
        }
     * 
     * 
     * */
}

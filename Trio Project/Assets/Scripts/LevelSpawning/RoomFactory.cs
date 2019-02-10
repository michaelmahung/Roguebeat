/*
 * This is where ill hold all the logic for grabbing differnet room types
 * one thing to look at here is that we are loading from the resources folder
 * so the overhead of this script on startup can be pretty big depending on 
 * how many rooms we end up creating
 * */


using UnityEngine;

public class RoomFactory : MonoBehaviour {

    [SerializeField] private GameObject[] OpenRooms;
    [SerializeField] private GameObject[] BossRooms;
    [SerializeField] private GameObject[] StartingRooms;
    [SerializeField] private GameObject[] LRRooms;
    [SerializeField] private GameObject[] LRTRooms;
    [SerializeField] private GameObject[] LRBRooms;
    [SerializeField] private GameObject[][] AllRooms; //We need an array to hold all of our arrays in order to generate a random room type.

    private void Awake()
    {
        DontDestroyOnLoad(this); //Keeping this object from reinstantiating will reduce how many times we need to reload all these rooms.

        OpenRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Open Rooms");
        BossRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Boss Rooms");
        StartingRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Starting Rooms");
        LRRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LR Rooms");
        LRTRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRT Rooms");
        LRBRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRB Rooms");

        AllRooms = new GameObject[][] { OpenRooms, BossRooms, StartingRooms, LRRooms, LRTRooms, LRBRooms };
    }

    //Generate random numbers and grab a random room from a random room array
    public GameObject GrabRandomRoom()
    {
        int rand = Random.Range(0, AllRooms.Length);
        int rand2 = Random.Range(0, AllRooms[rand].Length);

        //Debug.Log(rand + " " + rand2);
        return AllRooms[rand][rand2];
    }

    public GameObject GrabRoom(LevelSpawning.RoomSpawnTypes room)
    {
        switch (room)
        {
            case LevelSpawning.RoomSpawnTypes.StartingRoom:
                return StartingRooms[Random.Range(0, StartingRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.OpenRoom:
                return OpenRooms[Random.Range(0, OpenRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.BossRoom:
                return BossRooms[Random.Range(0, BossRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.LRBRoom:
                return LRBRooms[Random.Range(0, LRBRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.LRTRoom:
                return LRTRooms[Random.Range(0, LRTRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.LRRoom:
                return LRRooms[Random.Range(0, LRRooms.Length)];

            case LevelSpawning.RoomSpawnTypes.RandomRoom:
                return GrabRandomRoom();

            default:
                Debug.Log("Error selecting room to spawn");
                return GrabRandomRoom();
        }
    }
}

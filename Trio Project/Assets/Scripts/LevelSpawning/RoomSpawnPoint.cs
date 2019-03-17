/*This class will contain the logic for the spawn points within the individual rooms.
 * Each spawn point will need to keep track of a few things
 *
 * 1. What do I do if I am overlapping with another room? (Flex on the room until one yields) --> Spawn a door in the gap
 * 2. What do I do if I'm not overlapping with another room? --> Pick a random object from your spawns and spawn it in the gap.
 * 3. How do I make sure my spawns wont interfere with another rooms spawns? --> spawn sequentially in order of the rooms placed.
 */


 //TODO - need to clean this up a bit
using UnityEngine;
using System.Collections.Generic;

public class RoomSpawnPoint : MonoBehaviour {

    public enum spawnPointType { Start, Open, Boss };
    public spawnPointType SpawnPointType;

    [SerializeField] public RoomSetter OtherRoom;
    [SerializeField] private RoomSpawnPoint otherSpawnPoint;
    [SerializeField] public RoomSetter MyRoom { get; private set; }

    [SerializeField] private GameObject[] _mySpawnableObjects;
    [HideInInspector] public GameObject[] MySpawnableObjects { get { return _mySpawnableObjects; } }

    [SerializeField] private byte flexFactor;
    [SerializeField] public bool ObjectSpawned { get; private set; }

    //Collider myCollider;

    private void Awake()
    {
        if (MySpawnableObjects == null)
        {
            _mySpawnableObjects = new GameObject[0];
        }

        flexFactor = (byte)Random.Range(0, byte.MaxValue);
    }

    public void SetMyRoom(RoomSetter room)
    {
        MyRoom = room;
    }

    //When two spawn points overlap, they will assign each other
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoomSpawnPoint>() != null)
        {
            otherSpawnPoint = other.GetComponent<RoomSpawnPoint>();
            otherSpawnPoint.otherSpawnPoint = this;
            OtherRoom = otherSpawnPoint.GetComponentInParent<RoomSetter>();
            otherSpawnPoint.OtherRoom = GetComponentInParent<RoomSetter>();

            if (otherSpawnPoint != null)
            {
                if (otherSpawnPoint.SpawnPointType == spawnPointType.Start || otherSpawnPoint.SpawnPointType == spawnPointType.Boss) //If im overlapping doors with the spawn room, make the room open so the player can leave it.
                {
                    //Destroy(otherSpawnPoint.gameObject);
                    Destroy(gameObject);
                    return;
                }
            }

            if (otherSpawnPoint != null) //If there is still a room overlapping mine, FLEX ON IT.
            {
                otherSpawnPoint.Flex(flexFactor);
                return;
            }
        }
    }

    //Using bytes because they use less memory - might be unnecessary but it made sense at the time.
    //This function will make a room flex at another room. If one room flexes harder than the other,
    //The losing room will destroy itself, and the winner will spawn a room where the struggle happened.
    public void Flex(byte otherFlex)
    {
        if (otherFlex > flexFactor)
        {
            Destroy(gameObject);
            return;
        } 
        else if (otherFlex == flexFactor)
        {
            if (otherSpawnPoint != null)
            {
                flexFactor = (byte)Random.Range(0, byte.MaxValue);
                otherSpawnPoint.Flex(flexFactor);
                return;
            }
        }

        GameObject door = Instantiate(MySpawnableObjects[0], transform.position,
        transform.rotation, transform);

        if (OtherRoom != null)
        {
            OtherRoom.MyDoors.Add(door.GetComponent<BaseDoor>());
        }

        ObjectSpawned = true;
    }

    //Will get called last and spawn random things inside the remaining spawn positions. 
    public void SpawnRandom()
    {
        if (!ObjectSpawned)
        {
            //Start at 1 to avoid spawning a door
            int rand = Random.Range(1, MySpawnableObjects.Length);

            try
            {
                Instantiate(MySpawnableObjects[rand], transform.position, transform.rotation, transform);
                //TODO -- Add things to spawns to avoid this
            }
            catch
            {
                //If theres only 1 thing in our listof things to spawn, spawn a door
                Instantiate(MySpawnableObjects[0], transform.position, transform.rotation, transform);
            }
        }
    }
}

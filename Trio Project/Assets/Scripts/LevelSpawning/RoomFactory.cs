using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomInfo
{
    public Vector3 RoomLocation;
    public GameObject RoomPrefab;
}


public class RoomFactory : MonoBehaviour {

    [SerializeField] private GameObject[] OpenRooms;
    [SerializeField] private GameObject[] BossRooms;
    [SerializeField] private GameObject[] StartingRooms;
    [SerializeField] private GameObject[] LRRooms;
    [SerializeField] private GameObject[] LRTRooms;
    [SerializeField] private GameObject[] LRBRooms;
    [SerializeField] private GameObject[][] AllRooms;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        OpenRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Open Rooms");
        BossRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Boss Rooms");
        StartingRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Starting Rooms");
        LRRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LR Rooms");
        LRTRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRT Rooms");
        LRBRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRB Rooms");

        AllRooms = new GameObject[][] { OpenRooms, BossRooms, StartingRooms, LRRooms, LRTRooms, LRBRooms };
    }

    public GameObject GrabRandomRoom()
    {
        int rand = Random.Range(0, AllRooms.Length);
        int rand2 = Random.Range(0, AllRooms[rand].Length);

        //Debug.Log(rand + " " + rand2);
        return AllRooms[rand][rand2];
    }

    public GameObject GrabOpenRoom() { return OpenRooms[Random.Range(0, OpenRooms.Length)];}

    public GameObject GrabBossRoom() { return BossRooms[Random.Range(0, OpenRooms.Length)];}

    public GameObject GrabStartingRoom() { return StartingRooms[Random.Range(0, OpenRooms.Length)]; }

    public GameObject GrabLRRoom() { return LRRooms[Random.Range(0, OpenRooms.Length)];}

    public GameObject GrabLRTRoom() { return LRTRooms[Random.Range(0, OpenRooms.Length)];}

    public GameObject GrabLRBRoom() { return LRBRooms[Random.Range(0, OpenRooms.Length)];}
}

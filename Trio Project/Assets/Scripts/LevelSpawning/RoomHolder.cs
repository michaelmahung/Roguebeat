using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHolder : MonoBehaviour {

    [SerializeField] private GameObject[] OpenRooms;
    [SerializeField] private GameObject[] BossRooms;
    [SerializeField] private GameObject[] StartingRooms;
    [SerializeField] private GameObject[] LRRooms;
    [SerializeField] private GameObject[] LRTRooms;
    [SerializeField] private GameObject[] LRBRooms;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        OpenRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Open Rooms");
        BossRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Boss Rooms");
        StartingRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/Starting Rooms");
        LRRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LR Rooms");
        LRTRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRT Rooms");
        LRBRooms = Resources.LoadAll<GameObject>("LevelSpawningRooms/LRB Rooms");
    }

    public GameObject GrabOpenRoom()
    {
        return OpenRooms[Random.Range(0, OpenRooms.Length)];
    }

    public GameObject GrabBossRoom()
    {
        return BossRooms[Random.Range(0, OpenRooms.Length)];
    }

    public GameObject GrabStartingRoom()
    {
        return StartingRooms[Random.Range(0, OpenRooms.Length)];
    }

    public GameObject GrabLRRoom()
    {
        return LRRooms[Random.Range(0, OpenRooms.Length)];
    }

    public GameObject GrabLRTRoom()
    {
        return LRTRooms[Random.Range(0, OpenRooms.Length)];
    }

    public GameObject GrabLRBRoom()
    {
        return LRBRooms[Random.Range(0, OpenRooms.Length)];
    }
}

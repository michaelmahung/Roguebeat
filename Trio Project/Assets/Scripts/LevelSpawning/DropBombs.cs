using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{

    public int TotalBombs;
    public int CurrentBombs;

    private RoomSetter MyRoom;

    public GameObject[] Bombspawners;
    public GameObject BombPrefab;
    private float WaitToSpawn;

    //public int BombLimit;

    // Use this for initialization
    void Start()
    {
        CurrentBombs = 0;
        WaitToSpawn = 0;
        MyRoom = GetComponent<RoomSetter>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBombs >= TotalBombs)
        {
            RoomManager.Instance.AddToDoor(MyRoom, RoomManager.RoomType.Timed);
            Debug.Log("Went through");
        }

        else
        {
            WaitToSpawn += Time.deltaTime;
            if (WaitToSpawn >= 0.5f)
            {
                int random = Random.Range(0, Bombspawners.Length);
                GameObject bomb = Instantiate(BombPrefab, Bombspawners[random].transform.position, Bombspawners[random].transform.rotation);
                bomb.GetComponent<Rigidbody>().AddRelativeForce(transform.up * 20000);
                CurrentBombs++;
                WaitToSpawn = 0;
            }
        }

    }
}

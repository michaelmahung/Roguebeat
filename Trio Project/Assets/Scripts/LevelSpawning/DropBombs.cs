using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour, IRoomBehaviour
{
    //Adding some logic to avoid repeated calls

    public int TotalBombs;
    public int CurrentBombs;

    private RoomSetter MyRoom;

    public GameObject[] Bombspawners;
    public GameObject BombPrefab;
    private float WaitToSpawn;

    public bool RoomActive { get; set; }
    bool finished; //tracking if the rooms already been finished

    //public int BombLimit;

    // Use this for initialization
    void Start()
    {
        CurrentBombs = 0;
        WaitToSpawn = 0;
        MyRoom = GetComponent<RoomSetter>();
    }

    void RoomFinished()
    {
        finished = true;
        RoomManager.Instance.AddToDoor(MyRoom, RoomManager.RoomType.Timed);
    }

    public void StartBehaviour()
    {
        //Debug.Log("Dropping bombs");
        RoomActive = true;
    }

    public void StopBehaviour()
    {
        RoomActive = false;
    }

    public void ResetBehaviour()
    {
        finished = false;
        CurrentBombs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished  && RoomActive)
        {
            if (CurrentBombs >= TotalBombs)
            {
                RoomFinished();
            }

            else
            {
                WaitToSpawn += Time.deltaTime;
                if (WaitToSpawn >= 0.5f)
                {
                    int random = Random.Range(0, Bombspawners.Length);

                    GameObject bomb = GenericPooler.Instance.GrabPrefab(PooledObject.RoomBomb);
                    bomb.transform.position = Bombspawners[random].transform.position;
                    bomb.transform.rotation = Bombspawners[random].transform.rotation;
                    bomb.SetActive(true);

                    //GenericPooler(BombPrefab, Bombspawners[random].transform.position, Bombspawners[random].transform.rotation);
                    //bomb.GetComponent<Rigidbody>().AddRelativeForce(transform.up * 20000); //Done in the bombs 
                    CurrentBombs++;
                    WaitToSpawn = 0;
                }
            }
        }
    }
}

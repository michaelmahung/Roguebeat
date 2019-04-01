using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoom : MonoBehaviour, ITrackRooms, IRoomBehaviour {

    public bool RoomCleared { get; private set; }
    [SerializeField] TileBehaviour[] myTiles;
    [SerializeField] float timeToHeat = 3.0f;
    [SerializeField] float delayTime = 1f;
    [SerializeField] int tilesNeeded = 12;

    public RoomSetter MyRoom { get; set; }

    public bool RoomActive { get; set; }
    bool incrementTimer;
    int tilesHeated = 0;
    float tileTimer = 0;

	void Start () {

        //LevelSpawning.FinishedSpawningRooms += SetComponents;
        SetComponents();
	}

    private void Update()
    {
        if (incrementTimer && !RoomCleared)
        {
            if (tileTimer < delayTime)
            {
                tileTimer += Time.deltaTime;
            } else if (tileTimer >= delayTime)
            {
                tileTimer = 0;
                HeatTile();
            }
        }
    }

    void HeatTile()
    {
        if (!RoomCleared)
        {
            int rand = Random.Range(0, myTiles.Length);

            if (!myTiles[rand].TileSelected)
            {
                myTiles[rand].OverheatRoom(timeToHeat);
            }
            else if (!AllTilesOverheated())
            {
                HeatTile();
            } else
            {
                return;
            }
        }
    }

    bool AllTilesOverheated()
    {
        foreach (TileBehaviour tile in myTiles)
        {
            if (!tile.TileSelected)
            {
                return false;
            }
        }

        return true;
    }

    public void TileHeated(TileBehaviour quad)
    {
        tilesHeated++;

        if (tilesHeated >= tilesNeeded && !RoomCleared)
        {
            RoomCleared = true;
            RoomManager.Instance.AddToDoor(MyRoom, RoomManager.RoomType.Timed);

            foreach (TileBehaviour tile in myTiles)
            {
                tile.CancelOverheats();
            }
        }
    }

    public void StartBehaviour()
    {
        RoomActive = true;
        incrementTimer = true;
    }

    public void StopBehaviour()
    {
        RoomActive = false;
        incrementTimer = false;
    }

    public void ResetBehaviour()
    {
        tilesHeated = 0;

        foreach (TileBehaviour tile in myTiles)
        {
            tile.ResetTile();
        }
    }

    /*void CheckPlayerRoom()
    {
        if (GameManager.Instance.PlayerRoom == MyRoom)
        {
            incrementTimer = true;
        } else
        {
            incrementTimer = false;
        }
    }*/

    void SetComponents()
    {
        //RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
        MyRoom = GetComponent<RoomSetter>();
        myTiles = GetComponentsInChildren<TileBehaviour>();
        foreach (TileBehaviour quad in myTiles)
        {
            quad.ParentRoom = this;
        }
    }
}

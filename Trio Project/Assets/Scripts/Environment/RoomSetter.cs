using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomSetter : MonoBehaviour
{

    public string RoomName;
    public List<BaseDoor> MyDoors = new List<BaseDoor>();

    public bool StartRoom;
    public bool EndRoom;
    public bool IsCleared { get; private set; }

    public SpawnEnemies[] MySpawners;
    public RoomSpawnPoint[] MyOpenWalls;
    [SerializeField] private Transform camPlacement;
    [SerializeField] private GameObject cam;

    public IRoomBehaviour RoomBehaviour { get; private set; }
    private CameraController2 camController;
    private RoomLight myLight;
    TagManager Tags;

    //public delegate void UpdateRoomDelegate();
    //public static event UpdateRoomDelegate UpdatePlayerRoom;

    void Awake()
    {
        RoomBehaviour = GetComponent<IRoomBehaviour>();

        if (string.IsNullOrEmpty(RoomName))
        {
            RoomName = gameObject.name;
        }

        camController = FindObjectOfType<CameraController2>();

        Tags = GameManager.Instance.Tags;
        cam = camController.gameObject;

        LevelSpawning.FinishedSpawningRooms += DelayedStart;
    }

    void Start()
    {
        PlayerHealth.PlayerKilled += OpenClearedDoors;
    }

    void DelayedStart()
    {
        Invoke("FinalizeRoom", 0.25f);
    }

    void FinalizeRoom()
    {
        LevelSpawning.FinishedSpawningRooms -= FinalizeRoom;
        //RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
        RoomManager.UpdatePlayerRoom += CheckPlayerRoom;
        MyOpenWalls = GetComponentsInChildren<RoomSpawnPoint>();

        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            point.SetMyRoom(this);
        }

        FindComponents();
    }

    private IEnumerator CloseDoors()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Closing Doors");

        foreach (BaseDoor door in MyDoors)
        {
            if (door != null)
            {
                door.CloseDoor();
            }
        }

        yield break;
    }

    private void OpenClearedDoors()
    {
        foreach (BaseDoor door in MyDoors)
        {
            if (door != null && door.DoorMovedOnce)
            {
                door.OpenDoor();
            }
        }
    }

    private void OpenDoors()
    {
        foreach (BaseDoor door in MyDoors)
        {
            if (door != null)
            {
                if (!door.DoorOpen)
                {
                    door.OpenDoor();
                    //Debug.Log("Opening Doors");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerTag))
        {
            //ITrackRooms roomTracker = other.GetComponent<ITrackRooms>();

            camController.SetFocalPoint(camPlacement.gameObject);

            if (myLight != null)
            {
                myLight.ToggleLight(true);
            }

            if (!IsCleared)
            {
                StartCoroutine(CloseDoors());
            }

            GameManager.Instance.PlayerRoom = this;
            RoomManager.Instance.UpdateRoom();
        }
    }

    void CheckPlayerRoom()
    {
        //Debug.Log(RoomBehaviour);

        if (RoomBehaviour != null)
        {

            if (GameManager.Instance.PlayerRoom == this)
            {
                RoomBehaviour.StartBehaviour();
            }
            else
            {
                if (RoomBehaviour.RoomActive)
                {
                    RoomBehaviour.StopBehaviour();
                }
            }

        }
    }

    public void RoomCleared()
    {
        IsCleared = true;

        foreach(BaseDoor door in MyDoors)
        {
            if (door != null)
            door.ResetDoor();
        }
    }

    void FindComponents()
    {
        BaseDoor[] _doors = GetComponentsInChildren<BaseDoor>();

        foreach (BaseDoor baseDoor in _doors)
        {
            if (baseDoor != null)
            {
                MyDoors.Add(baseDoor);
            }
        }

        MySpawners = GetComponentsInChildren<SpawnEnemies>();
        myLight = GetComponentInChildren<RoomLight>();
        MyOpenWalls = GetComponentsInChildren<RoomSpawnPoint>();

        if (camPlacement == null)
        {
            camPlacement = GameManager.Instance.PlayerObject.transform;
        }

        if (cam == null)
        {
            cam = FindObjectOfType<CameraController2>().gameObject;
        }

        if (MyDoors.Count > 0)
        {
            foreach (BaseDoor door in MyDoors)
            {
                door.AddRoom(this);
            }
        }

        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            point.SpawnRandom();
        }

        if (EndRoom)
        {
            FinishEndRoom();
            return;
        }

        if (StartRoom)
        {
            FinishStartRoom();
            return;
        }
    }

    void FinishStartRoom()
    {
        Debug.Log("Finalizing Start Room");
        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            if (point.OtherRoom != null)
            {
                StartDoor door = point.GetComponentInChildren<StartDoor>();

                if (door != null)
                {
                    //Debug.Log("Opening door");
                    door.AddRoom(this);
                    MyDoors.Add(door);
                    point.OtherRoom.MyDoors.Add(door);

                    if (StartRoom)
                    {
                        door.Invoke("OpenDoor", 1.5f);
                    }
                }
            }
        }
    }
    void FinishEndRoom()
    {
        Debug.Log("Finalizing End Room");

        foreach (RoomSpawnPoint point in MyOpenWalls)
        {
            if (point.OtherRoom != null)
            {
                EndDoor door = point.GetComponentInChildren<EndDoor>();

                if (door != null)
                {
                    door.AddRoom(this);
                    MyDoors.Add(door);
                    point.OtherRoom.MyDoors.Add(door);
                }
            }
        }
    }
}

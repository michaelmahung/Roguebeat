using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomSetter : MonoBehaviour {

    public string RoomName;
    public List<BaseDoor> MyDoors = new List<BaseDoor>();

    public bool IsCleared { get; private set; }

    public SpawnEnemies[] MySpawners;
    public RoomSpawnPoint[] MyOpenWalls;
    [SerializeField] private Transform camPlacement;
    [SerializeField] private GameObject cam;

    public IRoomBehaviour RoomBehaviour { get; private set; }
    private CameraController2 camController;
    private RoomLight myLight;
    TagManager Tags;

    public delegate void UpdateRoomDelegate();
    public static event UpdateRoomDelegate UpdatePlayerRoom;

    void Awake()
    {
        LevelSpawning.FinishedSpawningRooms += FinalizeRoom;
        camController = FindObjectOfType<CameraController2>();

        Tags = GameManager.Instance.Tags;
        cam = camController.gameObject;
    }

    void Start () {

        RoomBehaviour = GetComponent<IRoomBehaviour>();

        PlayerHealth.PlayerKilled += OpenDoors;

        if (string.IsNullOrEmpty(RoomName))
        {
            RoomName = gameObject.name;
        }
    }

    void FinalizeRoom()
    {
        LevelSpawning.FinishedSpawningRooms -= FinalizeRoom;
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
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

        foreach (BaseDoor door in MyDoors)
        {
            door.CloseDoor();
        }

        yield break;
    }

    private void OpenDoors()
    {
        foreach(BaseDoor door in MyDoors)
        {
            if (door.DoorMoved)
            door.OpenDoor();
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

            if (IsCleared == false)
            {
                StartCoroutine(CloseDoors());
            }

            UpdatePlayer();
        }
    }

    public void UpdatePlayer()
    {
        GameManager.Instance.PlayerRoom = this;

        if (UpdatePlayerRoom != null)
        {
            UpdatePlayerRoom();
        } else
        {
            CheckPlayerRoom();
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
    }

    void FindComponents()
    {
        BaseDoor[] _doors = GetComponentsInChildren<BaseDoor>();

        foreach(BaseDoor baseDoor in _doors)
        {
            MyDoors.Add(baseDoor);
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
    }
}

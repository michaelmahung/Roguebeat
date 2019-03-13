using UnityEngine;
using System.Collections.Generic;

//Abstract classes are not able to be attached to GameObjects.
//To me, abstract classes are simply blueprints for their child classes, and are not meant to function on their own.

public abstract class BaseDoor : MonoBehaviour, ITrackRooms
{
    [SerializeField] private List<RoomSetter> myRooms = new List<RoomSetter>();
    public RoomSetter MyRoom { get; set; }
    public enum moveAxis { X, Y, Z }
    public bool DoorMoved { get; private set; }

    [SerializeField] private moveAxis MoveAxis = moveAxis.Y;
    [SerializeField] private int OpenPoints;
    [SerializeField] private float moveAmount = 10;
    [SerializeField] private int objectsRequired = 3;
    [SerializeField] private int killsRequired = 5;

    protected int objectsDestroyed;
    protected int killCount;
    protected Vector3 moveDirection;
    protected bool doorOpen;


    public void Start()
    {
        //Switch/Do:Case statements are similar to if/else statements.
        //One key difference is that switch statements do not take comparators - No == or != or >=, any of that.
        //The benefit to switch statements is that theyre faster and easy to set for integer values.
        //Because each value of an enum is assigned an integer, a switch works very well.

        //switch (value) defines what we want to make a switch on.
        switch (MoveAxis)
        {
            //case reads the value and executes the code directly underneath it.
            case moveAxis.X:
                moveDirection = new Vector3(moveAmount, 0, 0);
                break;
                //After the code is run, break out of the switch statement.
            case moveAxis.Y:
                moveDirection = new Vector3(0, moveAmount, 0);
                break;
            case moveAxis.Z:
                moveDirection = new Vector3(0, 0, moveAmount);
                break;
                //If somehow none of the values are found within the switch, run the default code instead - this is equivalent to the final else of an if/else statement.
            default:
                moveDirection = new Vector3(0, 0, moveAmount);
                break;
        }

        OpenPoints = 150;

    }

    void ResetDoor()
    {
        objectsDestroyed = 0;
        killCount = 0;
    }

    public void AddRoom(RoomSetter room)
    {
        myRooms.Add(room);
    }

    public virtual void OpenDoor()
    {
        if (!doorOpen)
        {
            DoorMoved = true;
            doorOpen = true;
            RoomSetter _playerRoom = GameManager.Instance.PlayerRoom;

            if (!_playerRoom.IsCleared)
            {
                _playerRoom.RoomCleared();
                RoomManager.Instance.RemoveSpawners(_playerRoom);
                GameManager.Instance.AddScore(OpenPoints);
            }

            transform.localPosition += moveDirection;
        }
    }

    public virtual void CloseDoor()
    {
        if (doorOpen)
        {
            RoomSetter _playerRoom = GameManager.Instance.PlayerRoom;

            if (!_playerRoom.IsCleared)
            {
                doorOpen = false;
                ResetDoor();
                transform.localPosition -= moveDirection;
            }
        }
    }

    public virtual void ObjectDestroyed()
    {
        objectsDestroyed++;

        if (objectsDestroyed >= objectsRequired && !doorOpen)
        {
            OpenDoor();
        }
    }

    public virtual void EnemyKilled()
    {
        killCount++;

        if (killCount >= killsRequired && !doorOpen)
        {
            OpenDoor();
        }
    }

    public virtual void MiniBossKilled()
    {
        OpenDoor();
    }
}

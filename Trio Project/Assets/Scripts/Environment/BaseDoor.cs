using UnityEngine;
using System.Collections.Generic;

//Abstract classes are not able to be attached to GameObjects.
//To me, abstract classes are simply blueprints for their child classes, and are not meant to function on their own.

public abstract class BaseDoor : MonoBehaviour, ITrackRooms
{
    [SerializeField] private List<RoomSetter> myRooms = new List<RoomSetter>();
    public RoomSetter MyRoom { get; set; }
    public enum moveAxis { X, Y, Z }
    public bool DoorOpen { get; private set; }
    public bool DoorMovedOnce { get; private set; }

    [SerializeField] private moveAxis MoveAxis = moveAxis.Y;
    [SerializeField] protected int OpenPoints;

    //These should be dictated by the room itself, will change once i stabilize the new starting room logic TODO
    [SerializeField] private float moveAmount = 10;
    [SerializeField] private int objectsRequired = 3;
    [SerializeField] private int killsRequired = 5;

    protected int objectsDestroyed;
    protected int killCount;
    protected Vector3 moveDirection;
    protected bool doorCompleted;
    protected RoomSetter playerRoom;


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
    }

    public void ResetDoor()
    {
        doorCompleted = false;
        //DoorMoved = false;
        objectsDestroyed = 0;
        killCount = 0;

        if (!DoorOpen)
        {
            DoorOpen = true;
            transform.localPosition += moveDirection;
        }
    }

    public void AddRoom(RoomSetter room)
    {
        myRooms.Add(room);
    }

    public virtual void OpenDoor()
    {
        DoorMovedOnce = true;

        if (!DoorOpen)
        {
            DoorOpen = true;
            transform.localPosition += moveDirection;

            playerRoom = GameManager.Instance.PlayerRoom;
            //Debug.Log(playerRoom);

            if (!playerRoom.IsCleared && !doorCompleted && playerRoom != null)
            {
                doorCompleted = true;
                playerRoom.RoomCleared();
                RoomManager.Instance.RemoveSpawners(playerRoom);
                GameManager.Instance.AddScore(OpenPoints);
                //ResetDoor();
            }
        }
    }

    public virtual void CloseDoor()
    {
        if (DoorOpen)
        {
            playerRoom = GameManager.Instance.PlayerRoom;

            //Debug.Log(playerRoom.IsCleared + " door completed " + doorCompleted);

            if (!playerRoom.IsCleared && !doorCompleted)
            {
                DoorOpen = false;
                transform.localPosition -= moveDirection;
            }
        }
    }

    public virtual void ObjectDestroyed()
    {
        if (!DoorOpen && !doorCompleted)
        {
;            objectsDestroyed++;

            if (objectsDestroyed >= objectsRequired && !DoorOpen)
            {
                OpenDoor();
            }
        }
    }

    public virtual void EnemyKilled()
    {
        if (!DoorOpen && !doorCompleted)
        {
            killCount++;

            if (killCount >= killsRequired && !DoorOpen)
            {
                OpenDoor();
            }
        }
    }

    public virtual void MiniBossKilled()
    {
        if (!DoorOpen && !doorCompleted)
        {
            OpenDoor();
        }
    }
}

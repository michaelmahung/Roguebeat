using UnityEngine;
using System.Collections.Generic;

//Abstract classes are not able to be attached to GameObjects.
//To me, abstract classes are simply blueprints for their child classes, and are not meant to function on their own.

public abstract class BaseDoor : MonoBehaviour, ITrackRooms
{
    public string MyRoomName { get; set; }
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


    //Virtual functions are functions that I know I will want to change the functionality of for the children of this class.
    //For instance, if I didnt want a door to be removed from the GameManagers list I could do the following in a child class:
    /*
    public override void OpenDoor()
    {
        doorMoved = true;
        transform.localPosition += moveDirection;
    }

    //Another example could be how different people read. If I had a Human class that had a Read() function.
    //The Westeners would do Read(Left to Right)
    //And the Easterners would do Read(Right to Left)
    //Both can read, but they way they go about reading is different.
    */

    //Doing this allows me to redefine what opening the door means for the child.

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
            //When this door is open, remove it from the total list of active doors, this will make it easier to find the other doors when searching.
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


    //I also want to make this virtual because I know I might want to change what the door requires in order to open.

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

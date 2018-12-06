using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDoor : MonoBehaviour, ITrackRooms
{
    public string CurrentRoom { get; set; }
    public enum moveAxis { X, Y, Z }
    public moveAxis MoveAxis;
    public enum openCondition { Kills, Objectives, Objects }
    public openCondition OpenCondition;
    public float moveAmount = 10;
    public int thingsRequired;
    protected int thingsDestroyed;
    protected Vector3 moveDirection;
    protected bool doorMoved;


    public void Start()
    {
        switch (MoveAxis)
        {
            case moveAxis.X:
                moveDirection = new Vector3(moveAmount, 0, 0);
                break;
            case moveAxis.Y:
                moveDirection = new Vector3(0, moveAmount, 0);
                break;
            case moveAxis.Z:
                moveDirection = new Vector3(0, 0, moveAmount);
                break;
            default:
                moveDirection = new Vector3(0, 0, moveAmount);
                break;
        }
    }

    public virtual void OpenDoor()
    {
        doorMoved = true;
        transform.localPosition += moveDirection;
        GameManager.Instance.RemoveDoor(this);
    }

    public virtual void AddToDoor()
    {
        thingsDestroyed++;

        if (thingsDestroyed >= thingsRequired && !doorMoved)
        {
            OpenDoor();
        }
    }
}

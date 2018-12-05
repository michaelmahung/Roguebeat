using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDoor : MonoBehaviour
{
    public enum moveAxis { X, Y, Z }
    public moveAxis MoveAxis;
    public float moveAmount = 10;
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
    }
}

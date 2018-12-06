using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Randocube : DamageableObject
{
    Rigidbody rb;

    public new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
    }

    public void CheckPlayerRoom()
    {
        if (GameManager.Instance.PlayerRoom == CurrentRoom)
        {
            UnFreeze();
        } else
        {
            Freeze();
        }
    }

    public void UnFreeze()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void Freeze()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public override void Kill()
    {
        RoomSetter.UpdatePlayerRoom -= CheckPlayerRoom;
        base.Kill();
    }
}

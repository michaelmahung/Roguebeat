using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //I WONT WORK WITHOUT A RIGIDBODY

public class Randocube : DamageableObject
{
    Rigidbody rb;

    //Our parent class (DamageableObject) has it's own start function so we need to specify that we are making our own start function.
    //To this we change -- public void Start() ----> public new void Start()
    public new void Start()
    {
        //Again, base.Start(); says I want to do what my parent does but I also want to do the stuff beneath it.
        base.Start();
        ItemType = myItemType.Wood;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        KillPoints = 10;

        //Check out the RoomSetter class to see where this logic exists. What I'm doing here is saying, 
        //Hey room setter, whenever you run your UpdatePlayerRoom event, also run this CheckPlayerRoom function.

        //TODO - roomsetter will control the behavior of the room - not individual components
        //RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
        RoomManager.UpdatePlayerRoom += CheckPlayerRoom;
        LevelSpawning.FinishedSpawningRooms += CheckPlayerRoom;
    }

    public void CheckPlayerRoom()
    {
        //If the players current room is also my room
        if (GameManager.Instance.PlayerRoom == MyRoom)
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
        //Unsubscribe from the event - ensures the CheckPlayerRoom function wont get called on a destroyed object.
        LevelSpawning.FinishedSpawningRooms -= CheckPlayerRoom;
        RoomManager.UpdatePlayerRoom -= CheckPlayerRoom;
        base.Kill();
    }
}

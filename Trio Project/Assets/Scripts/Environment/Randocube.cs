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
        itemType = ItemType.Wood;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        KillPoints = 10;

        //Check out the RoomSetter class to see where this logic exists. What I'm doing here is saying, 
        //Hey room setter, whenever you run your UpdatePlayerRoom event, also run this CheckPlayerRoom function.

        RoomSetter.UpdatePlayerRoom += CheckPlayerRoom;
    }

    public void CheckPlayerRoom()
    {
        //If the players current room is also my room
        if (GameManager.Instance.PlayerRoom == MyRoomName)
        {
            UnFreeze();
        } else
        {
            Freeze();
        }
    }

    void OnCollisionEnter(Collision col){
    if(col.gameObject.tag == "RoomLights"){
        print("hitting ceiling");
Physics.IgnoreCollision(this.GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
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
        RoomSetter.UpdatePlayerRoom -= CheckPlayerRoom;
        base.Kill();
    }
}

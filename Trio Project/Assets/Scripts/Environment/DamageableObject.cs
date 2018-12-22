using UnityEngine;

//This is the class that DamageableObjects will inherit from.

public class DamageableObject : DamageableEnvironmentItemParent
{ 
    public new void Start()
    {
        base.Start();
        itemType = ItemType.Metal;
        KillPoints = 75;
    }

    public override void Kill()
    {
        RoomManager.Instance.AddToDoor(MyRoom);
        base.Kill();
    }
}

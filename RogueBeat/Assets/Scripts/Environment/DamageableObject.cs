using UnityEngine;

//This is the class that DamageableObjects will inherit from.
[System.Serializable]
public class DamageableObject : DamageableEnvironmentItemParent
{ 
    public new void Start()
    {
        base.Start();
        ItemType = myItemType.Metal;
        KillPoints = 75;
    }

    public override void Kill()
    {
        RoomManager.Instance.AddToDoor(GameManager.Instance.PlayerRoom, RoomManager.RoomType.Object);
        base.Kill();
    }
}

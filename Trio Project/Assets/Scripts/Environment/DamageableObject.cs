using UnityEngine;

//This is the class that DamageableObjects will inherit from.

public class DamageableObject : DamageableEnvironmentItemParent
{ 
    public override void Kill()
    {
        GameManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Objects);
        base.Kill();
    }
}

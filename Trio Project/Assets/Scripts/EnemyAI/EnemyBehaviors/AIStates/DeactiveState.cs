using UnityEngine;
using StateStuff;

public class DeactiveState : State<MAIData> {

    private static DeactiveState _instance;

    private DeactiveState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DeactiveState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DeactiveState();
            }

            return _instance;
        }
    }


    public override void EnterState(MAIData _owner)
    {
        _owner.AIRigidbody.isKinematic = true;
        _owner.IsEnabled = false;
        Debug.Log("Entering Deactive State");
    }

    public override void UpdateState(MAIData _owner)
    {
        
    }

    public override void ExitState(MAIData _owner)
    {
        _owner.AIRigidbody.isKinematic = false;
        _owner.IsEnabled = true;
        Debug.Log("Exiting Deactive State");
    }

}

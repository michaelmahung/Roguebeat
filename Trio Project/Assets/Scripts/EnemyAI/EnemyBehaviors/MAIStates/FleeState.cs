using UnityEngine;
using StateStuff;

public class FleeState : State<MAIData> {

    private static FleeState _instance;

    private FleeState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FleeState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FleeState();
            }

            return _instance;
        }
    }


    public override void EnterState(MAIData _owner)
    {
        _owner.IsEnabled = false;
        Debug.Log("Entering Deactive State");
    }

    public override void UpdateState(MAIData _owner)
    {

    }

    public override void ExitState(MAIData _owner)
    {
        _owner.IsEnabled = true;
        Debug.Log("Exiting Deactive State");
    }
}

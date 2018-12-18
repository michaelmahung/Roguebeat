using UnityEngine;
using StateStuff;

public class IdleState : State<MAIData> {

    private static IdleState _instance;

    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
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

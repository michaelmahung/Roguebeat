using StateStuff;
using UnityEngine;

public class ChaseState : State<MAIData> {

    private static ChaseState _instance;

    private ChaseState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ChaseState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ChaseState();
            }

            return _instance;
        }
    }


    public override void EnterState(MAIData _owner)
    {
        Debug.Log("Entering Chase State");
    }

    public override void UpdateState(MAIData _owner)
    {
        _owner.LookAtPlayer();
        _owner.ChasePlayer();
    }

    public override void ExitState(MAIData _owner)
    {
        Debug.Log("Exiting Chase State");
    }
}

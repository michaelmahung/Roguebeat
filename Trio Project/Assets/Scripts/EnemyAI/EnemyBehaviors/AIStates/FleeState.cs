using StateStuff;
using UnityEngine;

public class FleeState : State<MAIData> {

    private static FleeState _instance;
    private float fleeTimer;

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
        Debug.Log("Entering Flee State");
        fleeTimer = 0;
    }

    public override void UpdateState(MAIData _owner)
    {
        fleeTimer += Time.deltaTime;

        if (fleeTimer > 7)
        {
            _owner.stateMachine.ChangeState(IdleState.Instance);
        }

        _owner.RunFromPlayer();
    }

    public override void ExitState(MAIData _owner)
    {
        Debug.Log("Exiting Flee State");
    }


}

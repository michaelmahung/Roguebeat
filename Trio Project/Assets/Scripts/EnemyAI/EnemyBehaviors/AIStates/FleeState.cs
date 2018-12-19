using StateStuff;
using UnityEngine;

public class FleeState : State<AI> {

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

    public override void EnterState(AI _owner)
    {
        //Debug.Log("Entering Flee State");
        fleeTimer = 0;
    }

    public override void UpdateState(AI _owner)
    {
        fleeTimer += Time.deltaTime;

        if (fleeTimer > 5)
        {
            _owner.stateMachine.ChangeState(IdleState.Instance);
            _owner.HasFleed = true;
        }

        _owner.Flee();
    }

    public override void ExitState(AI _owner)
    {
        //Debug.Log("Exiting Flee State");
    }


}

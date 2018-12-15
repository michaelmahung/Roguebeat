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
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);

        if (distance < _owner.AttackRange)
        {
            _owner.stateMachine.ChangeState(AttackState.Instance);
        }

        _owner.LookAtPlayer();
        _owner.ChasePlayer();

        if (_owner.HealthPercent < 30)
        {
            if (_owner.Flees)
            {
                _owner.stateMachine.ChangeState(FleeState.Instance);
            }
            _owner.stateMachine.ChangeState(EnrageState.Instance);
        }
    }

    public override void ExitState(MAIData _owner)
    {
        Debug.Log("Exiting Chase State");
    }
}

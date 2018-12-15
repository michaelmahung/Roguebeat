using StateStuff;
using UnityEngine;

public class AttackState : State<MAIData> {

    private static AttackState _instance;

    private AttackState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AttackState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AttackState();
            }

            return _instance;
        }
    }


    public override void EnterState(MAIData _owner)
    {
        Debug.Log("Entering Attack State");
    }

    public override void UpdateState(MAIData _owner)
    {
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);

        if (distance > _owner.AttackRange)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }

        _owner.LookAtPlayer();
        _owner.ChasePlayer();
        _owner.AttackPlayer();

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
        Debug.Log("Exiting Attack State");
    }
}

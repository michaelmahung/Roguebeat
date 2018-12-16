using StateStuff;
using UnityEngine;

public class AttackState : State<AI> {

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


    public override void EnterState(AI _owner)
    {
    }

    public override void UpdateState(AI _owner)
    {
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);
        /*if (distance > _owner.AttackRange)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }
        */
        _owner.isEngagingPlayer = true;
        _owner.lookAtPlayer();
        _owner.ChasePlayer();
        _owner.StartCoroutine(_owner.FireWeapon());

        if (_owner.HealthPercentage < 30)
        {
        _owner.Flees = true;
            if (_owner.Flees)
            {
                _owner.stateMachine.ChangeState(FleeState.Instance);
            }
            //_owner.stateMachine.ChangeState(EnrageState.Instance);
        }
    }

    public override void ExitState(AI _owner)
    {
    }
}

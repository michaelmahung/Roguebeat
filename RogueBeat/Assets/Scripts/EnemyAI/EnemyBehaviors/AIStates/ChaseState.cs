using StateStuff;
using UnityEngine;

public class ChaseState : State<AI> {

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


    public override void EnterState(AI _owner)
    {
        _owner.IsEnabled = true;
    }

    public override void UpdateState(AI _owner)
    {
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);
        if(_owner.gameObject.GetComponent<Bruiser_New>() && distance < _owner.RamRange)
        {
            _owner.stateMachine.ChangeState(RamState.Instance);
        }

        else if (!_owner.gameObject.GetComponent<Bruiser_New>() && distance < _owner.AttackRange)
        {
            _owner.stateMachine.ChangeState(AttackState.Instance);
        }

        _owner.lookAtPlayer();
        _owner.ChasePlayer();

        /*
        if (_owner.HealthPercentage < 30 &&_owner.Flees != null)
        {
            if (_owner.Flees == true && _owner.HasFleed == false)
            {
                _owner.stateMachine.ChangeState(FleeState.Instance);
                return;
            }
            _owner.stateMachine.ChangeState(EnrageState.Instance);
        }*/
    }

    public override void ExitState(AI _owner)
    {
    }
}

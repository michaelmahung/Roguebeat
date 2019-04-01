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
        //float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);

        _owner.lookAtPlayer();
        _owner.ChasePlayer();
        _owner.StartCoroutine(_owner.FireWeapon());

        //If the owners health is low and they have an assigned Flee behavior.
        //Check to see if the owner has fleed already or if they are supposed to enrage.
        if (_owner.HealthPercentage < 30 && _owner.Flees != null)
        {
            if (_owner.Flees == true && _owner.HasFleed == false)
            {
                _owner.stateMachine.ChangeState(FleeState.Instance);
                return;
            }
            _owner.stateMachine.ChangeState(EnrageState.Instance);
        }
    }

    public override void ExitState(AI _owner)
    {
    }
}

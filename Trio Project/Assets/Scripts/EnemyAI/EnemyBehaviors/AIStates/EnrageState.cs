using StateStuff;
using UnityEngine;

public class EnrageState : State<AI> {

    private static EnrageState _instance;

    private EnrageState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static EnrageState Instance
    {
        get
        {
            if (_instance == null)
            {
                new EnrageState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        if (!_owner.Enraged)
        {
            _owner.Enraged = true;
            _owner.EnemyAttackSpeed *= 1.5f;
            _owner.MoveSpeed *= 1.5f;
        }
    }

    public override void UpdateState(AI _owner)
    {
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);

        if (distance < _owner.AttackRange)
        {
            _owner.lookAtPlayer();
            _owner.ChasePlayer();
            _owner.StartCoroutine(_owner.FireWeapon());
        }
        else
        {
            _owner.lookAtPlayer();
            _owner.ChasePlayer();
        }
    }

    public override void ExitState(AI _owner)
    {
        //Debug.Log("Exiting Enrage State");
    }

}

using StateStuff;
using UnityEngine;

public class EnrageState : State<MAIData> {

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

    public override void EnterState(MAIData _owner)
    {
        _owner.AttackSpeed *= 1.5f;
        _owner.Speed *= 1.5f;
        Debug.Log("Entering Enrage State");
    }

    public override void UpdateState(MAIData _owner)
    {
        float distance = Vector3.Distance(_owner.transform.position, _owner.Hero.position);

        if (distance < _owner.AttackRange)
        {
            _owner.LookAtPlayer();
            _owner.ChasePlayer();
            _owner.AttackPlayer();
        }else
        {
            _owner.LookAtPlayer();
            _owner.ChasePlayer();
        }
    }

    public override void ExitState(MAIData _owner)
    {
        Debug.Log("Exiting Enrage State");
    }

}

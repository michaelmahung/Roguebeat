using StateStuff;
using UnityEngine;

public class RamState : State<AI>
{

    private static RamState _instance;

    private RamState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }


    public static RamState Instance
    {
        get
        {
            if (_instance == null)
            {
                new RamState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        _owner.HasRammed = false;
        _owner.lookAtPlayer();
        _owner.storeTime = 0;
        _owner.IsRamming = true;
    }

    public override void UpdateState(AI _owner)
    {
        //    
        _owner.storeTime += Time.deltaTime;
        if (_owner.HasRammed == true && _owner.gameObject.GetComponent<Bruiser_New>())
        {
            _owner.IsRamming = false;
            _owner.lookAtPlayer();
            _owner.stateMachine.ChangeState(IdleState.Instance);
        }

        else

        {
            if (_owner.gameObject.GetComponent<Bruiser_New>() && _owner.HasRammed == false)
            {
                _owner.StartCoroutine(_owner.RamPlayers());
                if (_owner.storeTime >= _owner.EnemyRamTime)
                {
                    _owner.HasRammed = true;
                }
            }

        }
    }

    public override void ExitState(AI _owner)
    {

    }
}

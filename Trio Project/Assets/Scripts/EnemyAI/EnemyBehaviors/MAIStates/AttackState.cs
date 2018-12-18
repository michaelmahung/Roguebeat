using UnityEngine;
using StateStuff;

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
        _owner.IsEnabled = false;
        Debug.Log("Entering Deactive State");
    }

    public override void UpdateState(MAIData _owner)
    {

    }

    public override void ExitState(MAIData _owner)
    {
        _owner.IsEnabled = true;
        Debug.Log("Exiting Deactive State");
    }
}

using UnityEngine;
using StateStuff;

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

using UnityEngine;
using StateStuff;

public class DeactiveState : State<AI> {

    private static DeactiveState _instance;

    private DeactiveState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DeactiveState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DeactiveState();
            }

            return _instance;
        }
    }


    public override void EnterState(AI _owner)
    {
        if (_owner.IsEnabled)
        {
            _owner.StopAllCoroutines();
            _owner.AIRigidbody.isKinematic = true;
            _owner.IsEnabled = false;
        }
        //Debug.Log("Entering Deactive State");
    }

    public override void UpdateState(AI _owner)
    {
        
    }

    public override void ExitState(AI _owner)
    {
        _owner.AIRigidbody.isKinematic = false;
        _owner.IsEnabled = true;
        //Debug.Log("Exiting Deactive State");
    }

}

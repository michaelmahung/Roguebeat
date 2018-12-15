﻿using UnityEngine;
using StateStuff;

public class IdleState : State<AI> {

    private static IdleState _instance;
    private float idleTimer;

    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        idleTimer = 0;
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState(AI _owner)
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > 3)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Idle State");
    }
}

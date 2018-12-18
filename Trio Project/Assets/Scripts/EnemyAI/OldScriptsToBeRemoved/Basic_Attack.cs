using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Basic_Attack : State<AI>
{

private static Basic_Attack _instance;
private Basic_Attack ()
	{
		if (_instance != null) {
		return;
		}

		_instance = this;
	}

	public static Basic_Attack Instance {
		get {
			if (_instance == null) {
				new Basic_Attack ();
			}
			return _instance;
		}
	}


	public override void EnterState (AI _owner)
	{
	Debug.Log("Entering Basic Attack State");
	}

	public override void UpdateState (AI _owner)
	{
		if (_owner.EnemyHealth <= 50) {

		}

	_owner.FireWeapon();
	_owner.lookAtPlayer();
	_owner.ChasePlayer();
	}

	public override void ExitState (AI _owner)
	{

	}
}


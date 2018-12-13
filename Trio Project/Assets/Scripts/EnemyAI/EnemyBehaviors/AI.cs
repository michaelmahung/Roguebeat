using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
public bool switchState = false;
public float gameTimer;
public int seconds = 0;
public Transform Hero;
private float MoveSpeed = 5.0f;

public StateMachine<AI> stateMachine { get; set; }

private void Start ()
	{
	stateMachine = new StateMachine<AI>(this);
	stateMachine.ChangeState(FirstState.Instance);
	Hero = GameObject.FindGameObjectWithTag ("Player").transform;

	gameTimer = Time.time;
	}

	private void Update ()
	{


		if (Time.time > gameTimer + 1) {
			gameTimer = Time.time;
			seconds++;
			stateMachine.ChangeState(SecondState.Instance);
			Debug.Log (seconds);
		}

		if (seconds == 5) {
		seconds = 0;
		switchState = !switchState;
		}

		stateMachine.Update();
	}


	public void lookAtPlayer ()
	{
	transform.LookAt(Hero);
	}

	public void ChasePlayer ()
	{
		transform.position += transform.forward * MoveSpeed * Time.deltaTime;
	}

}
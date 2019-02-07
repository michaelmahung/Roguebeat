using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public abstract class AI : MonoBehaviour, ITrackRooms, IDamageable<float>
{

public GameObject[] EnemyWeapons;
public Transform Hero; // Transform variable used to acquire the player.
public string MyRoomName { get; set; } // What room is the enemy in.
public RoomSetter MyRoom { get; set; }
private Color EnemyBaseColor; //Handles Color change on damage to enemy
public Rigidbody AIRigidbody;

//Floats
[Tooltip("Movement Speed Of Enemy")]
public float MoveSpeed; // base variable for all enemy movespeeds; is uniquely set on specific enemy class
[Tooltip("Amount Of Enemy Health")]
public float EnemyHealth; // base variable for all enemy health; is uniquely set on specific enemy class
protected float currentHealth;
public float HealthPercentage;
[Tooltip("Rate Of Fire Of Enemy")]
public float EnemyAttackSpeed; // base variable for all enemy attack speeds; is uniquely set on specific enemy class
[Tooltip("Time Length Of Damage Visual")]
public float HurtDuration = 0.5f; // duration of hurt visual
[Tooltip("Incremental Change Rate For Damage Visual")]
public float SmoothColor = 0.10f; //rate of color change for hurt visual
public float AttackRange = 30;
//Floats


// Ints
[Tooltip("How Many Points This Enemy Is Worth On Death")]
public int KillPoints;
public int seconds = 0;
// Ints


//Bools
public bool IsFiring; // bool created to assist a Coroutine of enemy fire and wait time before firing again, used in Enemy Engagement Class
public bool? Flees;
public bool HasFleed;
public bool Enraged;
public bool IsEnabled;
public bool SwitchState = false;
public bool Dead;
public int WeaponValue; // int to allow selection of enemy weapon prefabs within the EnemyWeapons array, used in Enemy Engagement Class
//Bools



public StateMachine<AI> stateMachine { get; set; }

    public virtual void Start()
    {
    	AIRigidbody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(IdleState.Instance);
        //HealthPercentage = (currentHealth / EnemyHealth) * 100; *****Moved to specific enemy starts As their assignments of stats happens AFTER this call, making it null
        Hero = GameManager.Instance.PlayerObject.transform;
		EnemyWeapons = Resources.LoadAll<GameObject> ("Prefabs/EnemyWeapons"); // Assigns the entire contents of the folder EnemyWeapons in the Resources folder to the EnemyWeapons array.
        EnemyBaseColor = gameObject.GetComponent<Renderer>().material.color;
        RoomSetter.UpdatePlayerRoom += CheckRoom;
        Invoke("FindMyRoom", 0.1f);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    void FindMyRoom()
    {
        MyRoom = GameObject.Find(MyRoomName).GetComponent<RoomSetter>();
    }


   //Handles Enemies getting hurt, dying, changing colors
   //***********************************************************************************************************************
    //Handles enemy death, adding score from killed enemies, and adding door kill points to open doors.

	public virtual void Damage (float damage) // function on enemies to read damage from fire from player, reads Damage from Interfaces script.
	{
        if (IsEnabled)
        {
            currentHealth -= damage;
            //StartCoroutine(LerpColor()); // begin lerping color to show damage to enemy
            UpdateHealthPercentage();
            if (currentHealth <= 0)
            {
                enemyDeath();
            }
        }
	}

	 IEnumerator LerpColor ()
	{
		float progress = 0; //instance float created on start of coroutine 
		float increment = SmoothColor / HurtDuration; //instance float created on start of coroutine
		while (progress < 1) 
		{
		gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, EnemyBaseColor, progress);
		progress += increment; //add to Float progress by an amount from smoothColor divided by hurtDuration
		yield return new WaitForSeconds(SmoothColor);
		}
	}


	public void enemyDeath ()
	{
        if (!Dead)
        {
            Dead = true;
            if (MyRoom != null)
            {
                MyRoom.RemoveEnemy();
            }
            RoomSetter.UpdatePlayerRoom -= CheckRoom;
            GameManager.Instance.AddScore(KillPoints);
            RoomManager.Instance.AddToDoor(MyRoom);
            //RoomManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Kills);
            Destroy(gameObject);
        }
    }
    //**********************************************************************************************************************


    //Handles Basic State functions for most enemies
    //**********************************************************************************************************************


	public virtual void lookAtPlayer()
    {
        transform.LookAt(Hero);
    }

	public virtual void ChasePlayer()
    {
        transform.localPosition += transform.forward * MoveSpeed * Time.deltaTime;
    }

	public virtual void Enrage()
    {

    }

    public virtual void Flee()
    {
		Vector3 direction = transform.position - Hero.transform.position;
        direction.y = 0;//transform.position.y;
        direction.Normalize();
        transform.LookAt(direction);
        transform.position += direction * MoveSpeed * Time.deltaTime;
    }

	public IEnumerator FireWeapon ()
	{
		yield return new WaitForSeconds (EnemyAttackSpeed);
		Instantiate (EnemyWeapons [WeaponValue], transform.position, transform.rotation);

        if (IsFiring == false)
        {
            IsFiring = true;
            StartCoroutine(FireWeapon());
        }
        else
        {
            StopAllCoroutines();
        }
	}

	//************************************************************************************

    private void CheckRoom ()
	{
		if (stateMachine.CheckPlayerRoom(this))
        {
            stateMachine.ChangeState(ChaseState.Instance);
            return;
        }
        stateMachine.ChangeState(DeactiveState.Instance);
	}

    public void UpdateHealthPercentage ()
	{
		HealthPercentage = (currentHealth / EnemyHealth) * 100;
	}

}
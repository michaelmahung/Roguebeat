﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using System; // to use public event Actions

public abstract class AI : MonoBehaviour, ITrackRooms, IDamageable<float>
{

    public PooledObject MyWeapon;
public GameObject[] EnemyWeapons;
public GameObject FiringPoint; // Gameobject that represents the firing point of the enemy, if applicable
public Transform Hero; // Transform variable used to acquire the player.
public RoomSetter MyRoom { get; set; }
private Color EnemyBaseColor; //Handles Color change on damage to enemy
public Rigidbody AIRigidbody;
public HealthBarCode HealthBar;


//Floats*****************************************************************************************************************
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

    //float variables spefically for the Bruiser enemy class
public float RamRange = 15;
public float RamSpeed = 30;
public float EnemyRamTime;
public float storeTime;
public float RamDamage;
//Floats*****************************************************************************************************************

WaitForSeconds AttackSpeed;
WaitForSeconds RamTime;


protected IDamageable<float> PlayerDamage;
public SpawnerRoomScript SpawnerRoom;
//Floats


// Ints
[Tooltip("How Many Points This Enemy Is Worth On Death")]
public int KillPoints;
public int seconds = 0;
public int WeaponValue; // int to allow selection of enemy weapon prefabs within the EnemyWeapons array, used in Enemy Engagement Class
// Ints

protected TagManager Tags; // Reference the Tag Manager script


//Bools***********************************************************************************************************************************
public bool IsFiring; // bool created to assist a Coroutine of enemy fire and wait time before firing again, used in Enemy Engagement Class
public bool? Flees;
public bool HasFleed;
public bool Enraged;
public bool IsEnabled;
public bool SwitchState = false;
public bool Dead;

    //Variables specifically for the Bruiser enemy class
public bool HasRammed;
public bool IsRamming;
//Bools************************************************************************************************************************************

//Public Event
public event Action<float> OnHealthPctChanged = delegate {};



public StateMachine<AI> stateMachine { get; set; }

    protected virtual void OnEnable()
    {
        Dead = false;
        currentHealth = EnemyHealth;
        UpdateHealthPercentage();
        HealthBar.HealthChange(HealthPercentage);
        Invoke("CheckRoom", 0.1f);
    }

    public virtual void Awake()
    {
        //SpawnerRoom = MyRoom.GetComponent<SpawnerRoomScript>();
    	AIRigidbody = GetComponent<Rigidbody>();
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(IdleState.Instance);
        Tags = GameManager.Instance.Tags;
        AttackSpeed = new WaitForSeconds(EnemyAttackSpeed);
        RamTime = new WaitForSeconds(EnemyRamTime);
        Hero = GameManager.Instance.PlayerObject.transform;
		//EnemyWeapons = Resources.LoadAll<GameObject> ("Prefabs/EnemyWeapons"); // Assigns the entire contents of the folder EnemyWeapons in the Resources folder to the EnemyWeapons array.
        EnemyBaseColor = gameObject.GetComponent<Renderer>().material.color;
        RoomManager.UpdatePlayerRoom += CheckRoom;
        SceneGenerator.LoadingNextLevel += ResetAI;
        //Invoke("CheckRoom", 0.1f);
    }

    private void Update()
    {
        stateMachine.Update();
    }

   //Handles Enemies getting hurt, dying, changing colors
   //***********************************************************************************************************************
    //Handles enemy death, adding score from killed enemies, and adding door kill points to open doors.

	public virtual void Damage (float damage) // function on enemies to read damage from fire from player, reads Damage from Interfaces script.
	{
        if (IsEnabled)
        {
            currentHealth -= damage;
            float currentHealthPct = (float)currentHealth/(float)EnemyHealth;
            OnHealthPctChanged(currentHealthPct);
            StartCoroutine(LerpColor()); // begin lerping color to show damage to enemy
            UpdateHealthPercentage();
            HealthBar.HealthChange(currentHealthPct);
            if (currentHealth <= 0)
            {
                enemyDeath();
            }
        }
	}

    //Function added by Mike -- needed to test level new loading at runtime
    void ResetAI()
    {
        RoomManager.UpdatePlayerRoom -= CheckRoom;
        SceneGenerator.LoadingNextLevel -= ResetAI;
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

            if (MyRoom != null && SpawnerRoom != null)
            {
                SpawnerRoom.RemoveEnemy(); //Changed to talk to the room behaviour
            }

            //RoomSetter.UpdatePlayerRoom -= CheckRoom;
            GameManager.Instance.AddScore(KillPoints);
            RoomManager.Instance.AddToDoor(GameManager.Instance.PlayerRoom, RoomManager.RoomType.Enemy); //Changed by Mike to specify what kind of addition was made to the door.
            //RoomManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Kills);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    //**********************************************************************************************************************


    //Handles Basic State functions for most enemies
    //**********************************************************************************************************************


	public virtual void lookAtPlayer()
    {
        Vector3 targetPosition = new Vector3(Hero.position.x, this.transform.position.y, Hero.position.z);
        this.transform.LookAt(targetPosition);
    }

	public virtual void ChasePlayer()
    {
        transform.localPosition += transform.forward * MoveSpeed * Time.deltaTime;
    }

public IEnumerator RamPlayers()
{
    //yield return new WaitForSeconds(RamTime);
    storeTime += Time.deltaTime;
    if(storeTime <= EnemyRamTime)
    {
        //AIRigidbody.AddForce(transform.forward * RamSpeed);
        transform.localPosition += transform.forward * RamSpeed * Time.deltaTime;
        yield return (RamTime);
        //yield return new WaitForSeconds(RamTime);//SEETHIS
        StopAllCoroutines();
    }
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
		yield return(AttackSpeed);

        if (gameObject.activeInHierarchy)
        {
            GameObject go = GenericPooler.Instance.GrabPrefab(MyWeapon);
            go.transform.position = FiringPoint.transform.position;
            go.transform.rotation = FiringPoint.transform.rotation;
            go.SetActive(true);
        }

        //Instantiate (EnemyWeapons [WeaponValue], FiringPoint.transform.position, FiringPoint.transform.rotation);

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
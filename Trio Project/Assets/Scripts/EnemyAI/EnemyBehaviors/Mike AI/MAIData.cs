using UnityEngine;
using StateStuff;


//The Data Model for the AI

public abstract class MAIData : MonoBehaviour, IDamageable<float>, IKillable, ITrackRooms {

    public bool IsEnabled;
    public bool SwitchState;
    public float Speed = 10;
    public float AttackSpeed = 1;
    public float MaxHealth = 10;
    public int KillPoints { get; set; }
    public string CurrentRoom { get; set; }

    protected Transform Hero;

    private float currentHealth;
    private bool isDead;

    public StateMachine<MAIData> stateMachine { get; set; }

    /*****************************************************/

    public virtual void Start()
    {
        stateMachine = new StateMachine<MAIData>(this);
        stateMachine.ChangeState(DeactiveState.Instance);
        currentHealth = MaxHealth;
        Hero = GameManager.Instance.Player.transform;
        //We still have the issue of deciding how the AI will know when the player is in it's room.
        //This method will make the AI check for the player and set its active/deactive state
        //The other method would be for the statemachine to work without generic types
        //Another method would be for the GameManager to check AI rooms, but that would involve keeping a list of all AI at all times.
        RoomSetter.UpdatePlayerRoom += CheckRoom;
        Invoke("CheckRoom", 0.1f);
    }

    public virtual void FixedUpdate()
    {
        if (stateMachine.currentState != null)
        {
            stateMachine.Update();
        }
    }

    public virtual void LookAtPlayer()
    {
        transform.LookAt(Hero);
    }

    public virtual void ChasePlayer()
    {
        transform.position +=  transform.forward * Speed * Time.deltaTime;
    }

    private void CheckRoom()
    {
        if (stateMachine.CheckPlayerRoom(this))
        {
            stateMachine.ChangeState(ChaseState.Instance);
            return;
        }
        stateMachine.ChangeState(DeactiveState.Instance);
    }

    public virtual void Damage(float damage)
    {
        if (IsEnabled)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public virtual void Kill()
    {
        if (!isDead)
        {
            isDead = true;
            RoomSetter.UpdatePlayerRoom -= CheckRoom;
            GameManager.Instance.AddScore(KillPoints);
            GameManager.Instance.AddToDoor(CurrentRoom, BaseDoor.openCondition.Kills);
            Destroy(gameObject);
        }
    }
}

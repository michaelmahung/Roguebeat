using UnityEngine;

//This is the Parent class for all Damageable Environment Objects.
//This class is a doozy, but this is why it is hidden deep, very deep

[System.Serializable] //Allow the variables of this class to be available in the inspector

[RequireComponent(typeof(Rigidbody), (typeof(Collider)))] //DO NOT LET THIS SCRIPT BE ATTACHED TO ANYTHING WITHOUT THESE COMPONENTS

//Like the BaseDoor class, this will simply be used as a blueprint for it's children classes. We don't necessarily want this class
//To end up being attached to GameObjects and used as is.
//Basically, this class is a sacrifice made to make it's children cleaner and more managable.

public abstract class DamageableEnvironmentItemParent : MonoBehaviour, IDamageable<float>, IKillable, ITrackRooms //I want to take damage, die, and track rooms.
{
    private float _damageTaken { get; set; }
    protected float damageTaken
    {
        //When someone tries to access this variable, simply give them the value of it.
        get { return _damageTaken; }
        set
        {
            if (value < 0f)
            {
                //If something is trying to set the value of _damageTaken to a negative number, set it to 0 instead.
                value = 0f;
            }
            _damageTaken = value;
        }
    }

    private float _reactDuration;
    protected float reactDuration
    {
        get { return _reactDuration; }
        set
        {
            if (value < 0f)
            {
                //Also dont want negatives here
                value = 0f;
            }
            _reactDuration = value;
        }
    }

    private float _duration;
    protected float duration
    {
        get { return _duration; }
        set
        {
            if (value > 2f)
            {
                //2 Seconds is the longest the object should take to return to normal colors after being damaged
                value = 2f;
            }
            _duration = value;

        }
    }

    [SerializeField]
    protected float CurrentHealth;

    [SerializeField]
    protected float BaseHealth;

    protected float maxHealth
    {
        get { return BaseHealth * GameManager.Instance.Difficulty; }
    }

    [SerializeField]
    protected float HealthPercent
    {
        get { return CurrentHealth / maxHealth; }
    }

    //Armor in this context is simple damage reduction.
    [SerializeField]
    protected int Armor;

    protected bool dead;
    [SerializeField]
    protected Color hurtColor;
    [SerializeField]
    protected Color armorColor;
    protected Color startColor;
    protected Color currentColor;
    protected Renderer objectRenderer;

    protected enum myItemType
    {
        Default,
        Wood,
        Metal
    }

    protected myItemType ItemType;

    public RoomSetter MyRoom { get; set; }
    public int KillPoints { get; set; } // Killable requires us to assign how many points for dying.

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
        KillPoints = 5;
        reactDuration = 1;
        objectRenderer = gameObject.GetComponent<Renderer>();
        startColor = objectRenderer.material.color;
        SetColors();

        if (gameObject.tag == null)
        {
            gameObject.tag = "Damageable";
        }
    }

    protected virtual void SetColors()
    {
        armorColor = Color.yellow;
        hurtColor = Color.red;
    }

    protected virtual void Update()
    {
        if (reactDuration < 1)
        {
            ReactToDamage();
        }
    }

    public virtual void Damage(float damage)
    {
        damageTaken = (damage - Armor);

        if (damageTaken > 0)
        {
            CurrentHealth -= damageTaken;

            if (CurrentHealth <= 0)
            {
                if (ItemType != myItemType.Default)
                {
                    SFXManager.Instance.PlaySound(ItemType.ToString() + "Break");
                }
                Kill();
            } else
            {
                //We want the thing hit to flash red after being hit and we do this with the duration.
                //When taking damage, the duration is set to the amount of damage taken after armor.
                //This way, stronger weapons have a more lasting reaction than weaker ones - up to a cap of 2 seconds.

                objectRenderer.material.color = hurtColor;
                if (ItemType != myItemType.Default)
                {
                    SFXManager.Instance.PlaySound(ItemType.ToString() + "Hit");
                }
                reactDuration = 0;
                duration = damageTaken;
            }
        }

        if (Mathf.Abs(damageTaken) < Mathf.Epsilon)
        {
            //If the damage weve taken is negated by our armor, flash yellow instead.

            objectRenderer.material.color = armorColor;
            if (ItemType != myItemType.Default)
            {
                SFXManager.Instance.PlaySound(ItemType.ToString() + "Armor");
            }
            reactDuration = 0;
            duration = 0.5f;
        }
    }

    public virtual void ReactToDamage()
    {
        //Set the current color variable to be the current transition between the objects current color and its original color.
        currentColor = Color.Lerp(objectRenderer.material.color, startColor, reactDuration);
        objectRenderer.material.color = currentColor;
        
        //This is not an exact science but it works for now, I set the reactDuration to 0 when taking damage and increment it a little every frame.

        reactDuration += Time.deltaTime / (duration * 0.5f);
    }

    public virtual void Kill()
    {
        if (!dead)
        {
            dead = true;
            GameManager.Instance.AddScore(KillPoints);
            Destroy(gameObject);
        }
    }
}

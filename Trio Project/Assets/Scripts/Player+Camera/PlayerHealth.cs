using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField] private float currentHealth;
    public float HealthPercent
    {
        get
        {
            return currentHealth / MaxHealth;
        }
    }
    private float MaxHealth = 100;

    private bool isPlayerDead;
    public bool IsPlayerDead
    {
        get { return isPlayerDead; }
        private set { isPlayerDead = value; }
    }

    public int KillPoints { get; set; }

    [SerializeField] private float DamageShakeAmount;
    [SerializeField] private float DamageShakeDuration;

    public delegate void OnPlayerDamaged();
    public static event OnPlayerDamaged UpdateHealth;
    public static event OnPlayerDamaged PlayerDamaged;

    public delegate void OnPlayerKilled();
    public static event OnPlayerKilled PlayerKilled;

    void Start()
    {
        KillPoints = 0;
        currentHealth = MaxHealth;
        GameManager.Instance.PlayerRespawned += ResetHealth;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Damage(10);
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        //HealthPercent = currentHealth / MaxHealth;

        if (GameManager.Instance.CameraShaker != null)
        {
            GameManager.Instance.CameraShaker.HeavyShake();
        }

        PlayerDamaged();
        UpdateHealth();

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void ResetHealth()
    {
        IsPlayerDead = false;
        currentHealth = MaxHealth;
        //HealthPercent = currentHealth / MaxHealth;
        UpdateHealth();
    }

    //MUST be public due to the IKillable interface
    public void Kill()
    {
        if (!IsPlayerDead)
        {
            System.GC.Collect();
            GameManager.Instance.AddScore(KillPoints);
            IsPlayerDead = true;
            PlayerKilled();
            gameObject.SetActive(false);
        }
    }
}

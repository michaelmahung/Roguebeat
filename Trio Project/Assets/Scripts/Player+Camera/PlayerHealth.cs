using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float MaxHealth = 100;
    public float HealthPercent;
    public bool IsPlayerDead;

    public int KillPoints { get; set; }

    public delegate void OnPlayerDamaged();
    public static event OnPlayerDamaged UpdateHealth;

    public delegate void OnPlayerKilled();
    public static event OnPlayerKilled PlayerKilled;

    void Start()
    {
        KillPoints = -500;
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
        HealthPercent = currentHealth / MaxHealth;
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
        HealthPercent = currentHealth / MaxHealth;
        UpdateHealth();
    }

    public void Kill()
    {
        if (!IsPlayerDead)
        {
            GameManager.Instance.AddScore(KillPoints);
            IsPlayerDead = true;
            PlayerKilled();
            gameObject.SetActive(false);
        }
    }
}

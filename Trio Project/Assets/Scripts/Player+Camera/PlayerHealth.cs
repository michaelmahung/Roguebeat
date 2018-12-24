using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float HealthPercent { get; set; }
    private float MaxHealth = 100;

    private bool IsPlayerDead;

    public int KillPoints { get; set; }

    public delegate void OnPlayerDamaged();
    public static event OnPlayerDamaged UpdateHealth;
    public static event OnPlayerDamaged PlayerDamaged;

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
        if (GameManager.Instance.cameraShaker != null)
        {
            GameManager.Instance.cameraShaker.ShakeMe(80, 0.1f);
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

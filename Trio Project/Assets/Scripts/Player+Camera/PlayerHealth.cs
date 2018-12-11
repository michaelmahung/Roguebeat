using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float MaxHealth = 100;
    public float HealthPercent;
    public bool IsPlayerDead;

    public delegate void OnPlayerDamaged();
    public static event OnPlayerDamaged PlayerDamaged;

    public delegate void OnPlayerKilled();
    public static event OnPlayerKilled PlayerKilled;

    void Start()
    {
        currentHealth = MaxHealth;
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
        PlayerDamaged();

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        IsPlayerDead = true;
        PlayerKilled();
        Destroy(gameObject);
    }
}

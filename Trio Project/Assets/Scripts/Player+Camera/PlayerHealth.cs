using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float MaxHealth = 10;
    public float HealthPercent;

    public delegate void PlayerDamagedDelegate();
    public static event PlayerDamagedDelegate PlayerDamaged;

    void Start()
    {
        currentHealth = MaxHealth;
        HealthPercent = currentHealth / MaxHealth;

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("Player will have limited functionality without a GameManager script in the scene.");
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        PlayerDamaged();
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

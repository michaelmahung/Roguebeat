using UnityEngine;


//Basic player health script 

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float maxHealth = 10;

    void Start()
    {
        currentHealth = maxHealth;

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("Player will have limited functionality without a GameManager script in the scene.");
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
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

using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField]
    private float currentHealth;
    public float maxHealth = 10;

    void Start()
    {
        currentHealth = maxHealth;
        //If theres a player in the scene but no GameManager for some reason, throw a warning
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("Player will have limited functionality without a GameManager script in the scene.");
        }
    }

    public void Damage(float damage)
    {
        //Since the player uses the damageable interface, we need to assign a damage function
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //Since the player uses the killable interface, we need to assign a kill function
        Destroy(gameObject);
    }
}

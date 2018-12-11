using UnityEngine.UI;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{
    private PlayerHealth HealthComponent;

    private void Start()
    {
        PlayerHealth.PlayerDamaged += UpdateHealthBar;
        HealthComponent = GameManager.Instance.Player.GetComponent<PlayerHealth>();
    }

    private void UpdateHealthBar()
    {

    }
}

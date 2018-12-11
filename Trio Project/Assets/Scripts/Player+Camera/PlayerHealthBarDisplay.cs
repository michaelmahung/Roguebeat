using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBarDisplay : MonoBehaviour
{
    private PlayerHealth HealthComponent;
    private Slider PlayerHealthBar;

    private void Start()
    {
        PlayerHealthBar = GetComponent<Slider>();
        HealthComponent = GameManager.Instance.Player.GetComponent<PlayerHealth>();
        PlayerHealth.PlayerDamaged += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        if (PlayerHealthBar != null)
        {
            PlayerHealthBar.normalizedValue = HealthComponent.HealthPercent;
        } else
        {
            print("No health bar found");
        }
    }
}

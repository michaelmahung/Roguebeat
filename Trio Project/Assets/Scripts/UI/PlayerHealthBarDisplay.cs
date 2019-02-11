using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBarDisplay : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Slider playerHealthBar;

    private void Start()
    {
        playerHealthBar = GetComponent<Slider>();
        playerHealth = GameManager.Instance.PlayerObject.GetComponent<PlayerHealth>();
        PlayerHealth.UpdateHealth += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.normalizedValue = playerHealth.HealthPercent;
        } else
        {
            print("No health bar found");
        }
    }
}

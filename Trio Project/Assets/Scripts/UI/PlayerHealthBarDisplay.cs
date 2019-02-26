using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthBarDisplay : MonoBehaviour
{
    [SerializeField] private Color lowHealthColor;
    [SerializeField] private Color mediumHealthColor;
    [SerializeField] private Color highHealthColor;
    [SerializeField] private Image healthBarImage;
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

        if (playerHealthBar.normalizedValue >= 0.7f)
        {
            healthBarImage.color = highHealthColor;
        }

        if (playerHealthBar.normalizedValue < .7f && playerHealthBar.normalizedValue >= .4f)
        {
            healthBarImage.color = mediumHealthColor;
        }

        if (playerHealthBar.normalizedValue < 0.4f)
        {
            healthBarImage.color = lowHealthColor;
        }
    }
}

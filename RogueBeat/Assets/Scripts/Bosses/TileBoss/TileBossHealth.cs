using UnityEngine;
using UnityEngine.UI;

public class TileBossHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField] float baseHealth;
    [SerializeField] float currentHealth;
    public GameObject SliderObject;
    [SerializeField] private Slider healthBar;
    public float MaxHealth { get { return GameManager.Instance.Difficulty * baseHealth; } }
    public float HealthPercent { get { return currentHealth / MaxHealth; } }
    public int KillPoints { get; set; }
    BossController mainController;


    private void Awake()
    {
        mainController = GetComponent<BossController>();
        KillPoints = 300;
        currentHealth = MaxHealth;
        SliderObject.SetActive(false);
    }

    public void SetValues()
    {
        currentHealth = MaxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        healthBar.value = HealthPercent;

        if (currentHealth <= 0)
        {
            Kill();
        }

        if (HealthPercent <= .7f && HealthPercent >= .3f)
        {
            mainController.ChangePhase(BossPhases.Phase2);
        }

        if (HealthPercent < .3f)
        {
            mainController.ChangePhase(BossPhases.Phase3);
        }

    }

    public void Kill()
    {
        Debug.Log("Im dead, oh no");
        BossManager.Instance.RemoveBoss(mainController);
        this.gameObject.SetActive(false);
    }
}

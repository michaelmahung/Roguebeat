using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBossHealth : MonoBehaviour, IDamageable<float>, IKillable
{
    [SerializeField] float baseHealth;
    [SerializeField] float currentHealth;
    public float maxHealth { get { return GameManager.Instance.Difficulty * baseHealth; } }
    public float healthPercent { get { return currentHealth / maxHealth; } }
    public int KillPoints { get; set; }
    BossController mainController;


    private void Awake()
    {
        mainController = GetComponent<BossController>();
        KillPoints = 300;
        currentHealth = maxHealth;
    }

    public void SetValues()
    {
        currentHealth = maxHealth;
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
        Debug.Log("Im dead, oh no");
        BossManager.Instance.RemoveBoss(mainController);
        this.gameObject.SetActive(false);
    }
}

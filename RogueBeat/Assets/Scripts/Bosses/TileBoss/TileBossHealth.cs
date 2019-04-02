using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBossHealth : MonoBehaviour
{
    [SerializeField] float baseHealth;
    [SerializeField] float currentHealth;
    public float maxHealth { get { return GameManager.Instance.Difficulty * baseHealth; } }
    public float healthPercent { get { return currentHealth / maxHealth; } }


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void SetValues()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

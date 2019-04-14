using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSideHealthBars : MonoBehaviour {

    public SideTurret turret;
    public Slider healthBar;
    public GameObject HB;
    public GameObject turretName;

	// Use this for initialization
	void Start () {
        //healthBar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.normalizedValue = turret.health / turret.maxHealth;
        if(turret.dead == true)
        {
            HB.SetActive(false);
            turretName.SetActive(false);
        }
        if(turret.dead == false)
        {
            HB.SetActive(true);
            turretName.SetActive(true);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMainHealthBar : MonoBehaviour {

    public MainController controller;
    public MainTurret turret;
    public Slider healthBar;
    public GameObject HB;
    public GameObject turretName;
    public Transform background;

    // Use this for initialization
    void Start () {

       

	}
	
	// Update is called once per frame
	void Update () {
		if (controller.phase == "Attack" && (turret.tooClose == true || controller.attackPhase == controller.maxAttackPhase))
        {
            background.localScale = new Vector3 (1.05f, 1.5f, 1);
        }
        else
        {
            background.localScale = new Vector3(1, 1, 1);
        }
        healthBar.normalizedValue = turret.health / turret.maxHealth;
	}
}

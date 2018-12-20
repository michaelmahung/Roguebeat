using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Boomer_New : AI {


	// Use this for initialization
	public override void Start () {
	base.Start();
		MoveSpeed = 2.0f * GameManager.Instance.Difficulty; // assigns base enemy move speed per Trooper
		EnemyHealth = 10.0f * GameManager.Instance.Difficulty; // assigns base enemy health per Trooper
		EnemyAttackSpeed = 2.0f * GameManager.Instance.Difficulty; // assigns base enemy attack speed per Trooper
		WeaponValue = 0; // assigns int value to 1 in reading the EnemyWeapons gameobject array in grandparent class EnemyDataModel, which reads from EnemyWeapons Folder
        KillPoints = 10;
        currentHealth = EnemyHealth;
		HealthPercentage = (currentHealth / EnemyHealth) * 100;
		
	}
}

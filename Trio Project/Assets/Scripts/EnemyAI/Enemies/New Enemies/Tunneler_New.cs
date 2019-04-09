using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunneler_New : AI
{

    public override void Awake()
    {
        base.Awake();
        Flees = true;
        MoveSpeed = 5.0f * GameManager.Instance.Difficulty; // assigns base enemy move speed per Trooper
        EnemyHealth = 10.0f * GameManager.Instance.Difficulty; // assigns base enemy health per Trooper
        EnemyAttackSpeed = 0.2f * GameManager.Instance.Difficulty; // assigns base enemy attack speed per Trooper
        WeaponValue = 8; // assigns int value to 1 in reading the EnemyWeapons gameobject array in grandparent class EnemyDataModel, which reads from EnemyWeapons Folder
        KillPoints = 10;
        currentHealth = EnemyHealth;
        HealthPercentage = (currentHealth / EnemyHealth) * 100;
    }
}

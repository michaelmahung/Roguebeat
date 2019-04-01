using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class Bruiser_New : AI{

	// Use this for initialization
	public override void Awake () {
	base.Awake();
		MoveSpeed = 3.0f * GameManager.Instance.Difficulty; // assigns base enemy move speed per Trooper
		EnemyHealth = 10.0f * GameManager.Instance.Difficulty; // assigns base enemy health per Trooper
		EnemyAttackSpeed = 0.2f * GameManager.Instance.Difficulty; // assigns base enemy attack speed per Trooper
		WeaponValue = 2; // assigns int value to 1 in reading the EnemyWeapons gameobject array in grandparent class EnemyDataModel, which reads from EnemyWeapons Folder
        KillPoints = 10;
		currentHealth = EnemyHealth;
		HealthPercentage = (currentHealth / EnemyHealth) * 100;
		RamDamage = 2.0f * GameManager.Instance.Difficulty;
	}

	private void OnTriggerEnter(Collider other)
	{
        PlayerDamage = other.gameObject.GetComponent<IDamageable<float>>();
        if(PlayerDamage != null && !other.CompareTag(Tags.EnemyTag) && !other.CompareTag(Tags.ShieldTag) && !other.CompareTag(Tags.ShieldTag) && !other.CompareTag(Tags.EnemyProjectileTag) && IsRamming == true)
        {
            PlayerDamage.Damage(gameObject.GetComponent<Bruiser_New>().RamDamage);
			IsRamming = false;
			HasRammed = true;
        }
        else if (other.CompareTag(Tags.WallTag))
        {
			 if (stateMachine.currentState == RamState.Instance)
            {
                IsRamming = false;
                HasRammed = true;
                StopAllCoroutines();
                stateMachine.ChangeState(IdleState.Instance);
			}
		}
	}
}

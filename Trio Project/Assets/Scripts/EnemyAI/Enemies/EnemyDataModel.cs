using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDataModel : MonoBehaviour , IDamageable<float> {

[HideInInspector]
public GameObject[] EnemyWeapons;
public Transform Hero;
public float MoveSpeed;
public float EnemyHealth;
public float EnemyAttackSpeed;
public bool IsFiring;
public int WeaponValue;


/*public MyStruct[] EnemyWeaponTypes; ****************************Struct Usage(Mike)
[System.Serializable]
public struct MyStruct {
[HideInInspector]
public string name;
public GameObject Weapon;
public int Health;
public float TimeToDie;
}
*/

	// Use this for initialization
	public void Start ()
	{
		Hero = GameObject.FindGameObjectWithTag ("Player").transform;
		EnemyWeapons = Resources.LoadAll<GameObject> ("Prefabs/EnemyWeapons");
		for (int i = 0; i < EnemyWeapons.Length; i++) {
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Damage (float damage)
	{
		EnemyHealth -= damage;
		if (EnemyHealth <= 0) {
			Destroy (gameObject);
		}
	}
}

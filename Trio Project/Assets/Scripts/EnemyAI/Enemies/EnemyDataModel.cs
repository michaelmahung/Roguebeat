using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDataModel : MonoBehaviour , IDamageable<float> {

public GameObject[] EnemyWeapons;
protected GameObject EnemyShotgun2;
protected GameObject BombPrefab;
public Transform Hero;
public float MoveSpeed;
public float EnemyHealth;
public float EnemyAttackSpeed;


/*public MyStruct[] EnemyWeaponTypes;
[System.Serializable]
public struct MyStruct {
[HideInInspector]
public string name;
public GameObject Weapon;
}
*/



	// Use this for initialization
	public void Start ()
	{
		Hero = GameObject.FindGameObjectWithTag ("Player").transform;
		EnemyWeapons = Resources.LoadAll("/Prefabs/EnemyWeapons", typeof(GameObject))
		.Cast<GameObject>()
		.ToArray();
		Debug.Log(EnemyWeapons[0].name);
		
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

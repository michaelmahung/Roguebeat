using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoors : MonoBehaviour, IDamageable<float>{

public int enemiesRequired;
public EnemyDataModel CountEnemyDeaths;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (enemiesRequired == 10) {
			transform.Translate (10, 0, 0);
			print ("moved");
		}
	}

	public void AddKills ()
	{
	enemiesRequired++;
	print(enemiesRequired);
	}

	public void Damage (float damage)
	{

	}

}

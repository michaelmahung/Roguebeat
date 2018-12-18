using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Engagement Class for enemies. Reads base variable data stored in parent class EnemyDataModel. Stores functions to be called in specific enemy classes.
/// </summary>
public class EnemyEngagement : EnemyDataModel {

    /*//Josh's shit
    private EnemyProjectileColorManager eColor;
    private int colorIndex;
    private Color[] getColor;
    private Color startColor = Color.red;*/

   /* //added by Josh
    private new void Start()
    {
        //also Josh's shit
        eColor = GetComponent<EnemyProjectileColorManager>();
        colorIndex = eColor.currentIndex;
        getColor = eColor.colors;
    }*/

    // Not a REAL start function, serves as typical function to be called from specific enemy classes. 
    public void Start2 () {
	base.Start(); // call to its parent class, EnemyDataModel, to ensure it runs it's full Start function first, to ensure it's Start calls reach the unique enemy classes.
        
        
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator FireWeapon () // 
	{
		yield return new WaitForSeconds(EnemyAttackSpeed);
        Instantiate (EnemyWeapons[WeaponValue], transform.position, transform.rotation);
        //start Josh's addition
        //Renderer rend = GetComponent<Renderer>();
        //EnemyWeapons[WeaponValue].GetComponent<Renderer>().sharedMaterial.color = Color.Lerp(startColor, getColor[colorIndex], Time.deltaTime); 
        //end Josh's addition
		StartCoroutine(FireWeapon());
	}

	public void SeePlayer ()
	{
        if (isEngagingPlayer)
        {
            transform.LookAt(Hero); // cause enemies to look at Player
            if (IsFiring == false)
            {
                IsFiring = true;
                StartCoroutine(FireWeapon());
            }
        } else
        {
            StopAllCoroutines();
        }
	}

	public void ChasePlayer ()
	{
        if (isEngagingPlayer)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
	}
}


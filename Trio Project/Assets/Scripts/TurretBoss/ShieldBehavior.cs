using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour, IDamageable<float> {

    public bool spawnShield;
    public bool shieldDown;
    public bool genShield;
    public int health;
    public Transform spawnPoint;
    //public ShieldController sC;
    //public GameObject shieldObject;
    
// Use this for initialization
void Start () {
        //health = 10;
        shieldDown = true;
	}
	
	// Update is called once per frame
	void Update () {
        MeshRenderer m = spawnPoint.GetComponent<MeshRenderer>();

		if (shieldDown == true)
        {
            transform.gameObject.tag = "Untagged";
            spawnPoint.transform.localScale = new Vector3(.0625f, 1, 1.885f);
            m.enabled = false;
        }
        if (shieldDown == false)
        {
            transform.gameObject.tag = "Shield";
        }

        if(health <= 0)
        {
            shieldDown = true;
            print("Shield down!");
        }
        /*if (sC.raiseShields == true)
        {
            genShield = true;
            //sC.raiseShields = false;
        }*/
            if (genShield == true)
            {
                // Generate();
                m.enabled = true;
                spawnPoint.transform.localScale = Vector3.Lerp(spawnPoint.transform.localScale, spawnPoint.transform.localScale * 2, Time.deltaTime * 1.35f);
                shieldDown = false;
            health = 10;
            //sC.raiseShields = false;
                Invoke("SwitchGen", .5f);
                //genShield = false;
            }
        
       
	}

    private void OnCollisionEnter(Collision other)
    {
      
    }

    public void Damage(float hurt)
    {
        // print("Im hit");
        health--;
    }

    void SwitchGen()
    {
        genShield = false;
    }
}

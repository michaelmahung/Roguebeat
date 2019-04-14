using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour, IDamageable<float> {
    public Material origMat;
    public Material newMat;
    public bool spawnShield;
    public bool shieldDown;
    public bool genShield;
    public bool changeColor;
    public int health;
    public Transform spawnPoint;
    public MainController controller;
    
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
            //print("Shield down!");
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
            if (controller.Difficulty >= 3)
            {
                health = 15;
            }
            else
            {
                health = 10;
            }
                Invoke("SwitchGen", .5f);
                
            }
            if(changeColor == true)
            {
            spawnPoint.GetComponent<MeshRenderer>().material = newMat;
            Invoke("ChangeBack", .1f);
            }
             


    }

    private void OnCollisionEnter(Collision other)
    {
      
    }

    public void Damage(float hurt)
    {
        // print("Im hit");
        health--;
        
        changeColor = true;
    }

    void SwitchGen()
    {
        genShield = false;
    }

    void ChangeBack()
    {
        spawnPoint.GetComponent<MeshRenderer>().material = origMat;
        changeColor = false;
    }
}

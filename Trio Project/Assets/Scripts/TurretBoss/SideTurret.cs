using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTurret : MonoBehaviour {

    public MainController controller;
    public GameObject body;

    public int health;
    public int maxHealth = 10;

    public bool disabled;
    public bool dead;

    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;

    public float atkTime;

    public GameObject spawn;

    public bool p1fire;
    public bool p2fire;
    public bool p3fire;

    public bool attacking;

    // Use this for initialization
    void Start () {

        controller = body.GetComponent<MainController>();

    }

    

    // Update is called once per frame
    void Update () {
		
        if(controller.phase == "Attack")
        {
           
            if(controller.attackPhase == 1)
            {
                atkTime = 1.0f;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseOne());
                }
                
            }

            if (controller.attackPhase == 2)
            {

            }

            if (controller.attackPhase == 3)
            {

            }


        }

	}

    public IEnumerator PhaseOne()
    {
        yield return new WaitForSeconds(atkTime);
        p1fire = false;
        GameObject fire;
        fire = Instantiate(shot1, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if(p1fire == false)
        {
            //print("I am shooting a bullet");
            p1fire = true;
            StartCoroutine(PhaseOne());
            
        }
        else
        {
            attacking = false;
            StopCoroutine(PhaseOne());
        }
    }

    void PhaseTwo()
    {

    }

    void PhaseThree()
    {

    }
}

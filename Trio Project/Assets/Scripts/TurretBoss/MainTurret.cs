using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour, IDamageable<float> {

    public SideTurret rTurret;
    public SideTurret lTurret;
    public MainController controller;
    public GameObject body;

    public int health = 30;

    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject burn;

    public float atkTime;

    public GameObject spawn;

    public bool p1fire;
    public bool p2fire;
    public bool p3fire;
    public bool burnFire;

    public bool attacking;
    public bool tooClose;
    public bool trueOnce;
    public bool exitRotation;
    public bool exitDone;


	// Use this for initialization
	void Start () {

        controller = body.GetComponent<MainController>();

	}
	
	// Update is called once per frame
	void Update () {
		if (controller.phase == "Attack" && tooClose == false)
        {
            
            
            if (controller.attackPhase == 1)
            {
                if (trueOnce == true)
                {
                    StopCoroutine(Flamethrower());
                    attacking = false;
                    trueOnce = false;
                }
                //print("I am in attack phase 1");
                atkTime = 1.0f;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseOne());
                }

            }

            if (controller.attackPhase == 2)
            {
                if (trueOnce == true)
                {
                    StopCoroutine(Flamethrower());
                    attacking = false;
                    trueOnce = false;
                }
                atkTime = .75f;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseTwo());
                }
            }

            if (controller.attackPhase == 3)
            {
                if (trueOnce == true)
                {
                    StopCoroutine(Flamethrower());
                    attacking = false;
                    trueOnce = false;
                }
                atkTime = 1.2f;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseThree());
                }
            }
        }
        if(controller.phase == "Attack" && tooClose == true)
        {
            if(trueOnce == false)
            {
                StopAllCoroutines();
                attacking = false;
                trueOnce = true;
            }
            atkTime = .025f;
            if(attacking == false)
            {
                
                attacking = true;
                StartCoroutine(Flamethrower());
            }
        }
        if(controller.phase != "Attack")
        {
            StopAllCoroutines();
            attacking = false;
        }

    }

    public IEnumerator PhaseOne()
    {
        yield return new WaitForSeconds(atkTime);
        p1fire = false;
        GameObject fire;
        fire = Instantiate(shot1, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (p1fire == false)
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

    public IEnumerator PhaseTwo()
    {
        print("I moved to phase 2");
        yield return new WaitForSeconds(atkTime);
        p2fire = false;
        GameObject fire;
        fire = Instantiate(shot2, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (p2fire == false)
        {
            //print("I am shooting a bullet");
            p2fire = true;
            StartCoroutine(PhaseTwo());

        }
        else
        {
            attacking = false;
            StopCoroutine(PhaseTwo());
        }
    }

    public IEnumerator PhaseThree()
    {
        yield return new WaitForSeconds(atkTime);
        p3fire = false;
        GameObject fire;
        fire = Instantiate(shot3, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (p3fire == false)
        {
            //print("I am shooting a bullet");
            p3fire = true;
            StartCoroutine(PhaseThree());

        }
        else
        {
            attacking = false;
            StopCoroutine(PhaseThree());
        }
    }

    public IEnumerator Flamethrower()
    {
        yield return new WaitForSeconds(atkTime);
        burnFire = false;
        GameObject fire;
        fire = Instantiate(burn, spawn.transform.position, spawn.transform.rotation) as GameObject;
        //print("BUUUURRRRN!!!");
        if (tooClose == true)
        {
            if (burnFire == false)
            {
                burnFire = true;
                StartCoroutine(Flamethrower());
            }
            else
            {
                attacking = false;
                StopCoroutine(Flamethrower());
            }
        }

    }

    public void Damage(float hurt)
    {
        if(rTurret.dead == true && lTurret.dead == true)
        {
            health--;
        }
    }
}

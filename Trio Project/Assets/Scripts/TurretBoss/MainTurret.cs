using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour, IDamageable<float> {

    public SideTurret rTurret;
    public SideTurret lTurret;
    public MainController controller;
    public GameObject body;
    public GameObject masterBody;
    public GameObject mBody;
    public GameObject cap;

    public int health = 30;

    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject burn;

    public float atkTime;
    public float p1Time;
    public float p2Time;
    public float p3Time;
    public float burnTime;
    public float p4Time;

    public GameObject spawn;

    public bool p1fire;
    public bool p2fire;
    public bool p3fire;
    public bool p4fire;
    public bool burnFire;

    public bool attacking;
    public bool tooClose;
    public bool trueOnce;
    public bool dead;
    public bool changeColor;


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
                atkTime = p1Time;
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
                atkTime = p2Time;
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
                atkTime = p3Time;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseThree());
                }
            }

            if (controller.attackPhase == 4)
            {
                if (trueOnce == true)
                {
                    StopCoroutine(Flamethrower());
                    attacking = false;
                    trueOnce = false;
                }
                atkTime = p4Time;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseFour());
                }
            }
        }
        
        if (controller.phase == "Attack" && tooClose == true)
        {
            if(trueOnce == false)
            {
                StopAllCoroutines();
                attacking = false;
                trueOnce = true;
            }
            atkTime = burnTime;
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

        if(health <= 0)
        {
            Dead();
        }
        if(changeColor == true)
        {
            //mBody.GetComponent<MeshRenderer>().material.color = Color.white;
            //cap.GetComponent<MeshRenderer>().material.color = Color.white;
            Invoke("ChangeBack", .1f);
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
        yield return new WaitForSeconds(atkTime);
        p2fire = false;
        GameObject fire;
        fire = Instantiate(shot2, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (p2fire == false)
        {
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
            p3fire = true;
            StartCoroutine(PhaseThree());

        }
        else
        {
            attacking = false;
            StopCoroutine(PhaseThree());
        }
    }

    public IEnumerator PhaseFour()
    {
        yield return new WaitForSeconds(atkTime);
        p4fire = false;
        GameObject fire;
        fire = Instantiate(shot1, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (p4fire == false)
        {
            p4fire = true;
            StartCoroutine(PhaseFour());

        }
        else
        {
            attacking = false;
            StopCoroutine(PhaseFour());
        }
    }

    public IEnumerator Flamethrower()
    {
        yield return new WaitForSeconds(atkTime);
        burnFire = false;
        GameObject fire;
        fire = Instantiate(burn, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if (tooClose == true)
        {
            if(controller.attackPhase == 4)
            {
               
            }
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
        if((rTurret.dead == true && lTurret.dead == true)||(controller.phase == "Attack" && tooClose == true))
        {
            health--;
            changeColor = true;
        }
    }

    public void Dead()
    {
        dead = true;
        Destroy(masterBody);
    }

    public void ChangeBack()
    {
        //mBody.GetComponent<MeshRenderer>().material.color = Color.red;
        //cap.GetComponent<MeshRenderer>().material.color = Color.red;
        changeColor = false;
    }
}

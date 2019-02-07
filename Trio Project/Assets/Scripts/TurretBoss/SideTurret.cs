﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTurret : MonoBehaviour, IDamageable<float> {

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
    public float p1Time;
    public float p2Time;
    public float p3Time;

    public GameObject spawn;
    public GameObject Tbody;
    public GameObject cap;
    public GameObject barrel;

    public bool p1fire;
    public bool p2fire;
    public bool p3fire;

    public bool attacking;

    // Use this for initialization
    void Start () {

        controller = body.GetComponent<MainController>();
        health = maxHealth;

    }

    

    // Update is called once per frame
    void Update () {
        
            if (controller.phase == "Attack" && disabled == false )
            {
            //print("im attacking you fucker");
                if (controller.attackPhase == 1)
                {
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
                    atkTime = p2Time;
                    if (attacking == false)
                         {
                            attacking = true;
                            StartCoroutine(PhaseTwo());
                        }
            }

                if (controller.attackPhase == 3)
                {
                atkTime = p3Time;
                if (attacking == false)
                {
                    attacking = true;
                    StartCoroutine(PhaseThree());
                }
            }



            }
        
        else
        {
            StopAllCoroutines();
            attacking = false;
        }
            if (health <= 0)
            {
                disabled = true;
            }
        if(controller.attackPhase == 3 && disabled == true)
        {
            dead = true;
            transform.gameObject.tag = "Untagged";
            DestroyPhys();
        }

	}

    public IEnumerator PhaseOne()
    {
        yield return new WaitForSeconds(atkTime);
        p1fire = false;
        GameObject fire;
        fire = Instantiate(shot1, spawn.transform.position, spawn.transform.rotation) as GameObject;
        if(p1fire == false )
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

    public void Damage(float hurt)
    {
        health--;
    }

    public void DestroyPhys()
    {
        Destroy(Tbody);
        Destroy(cap);
        Destroy(barrel);
        Destroy(spawn);
    }
}
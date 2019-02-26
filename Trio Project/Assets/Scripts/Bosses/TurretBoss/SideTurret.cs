using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideTurret : MonoBehaviour, IDamageable<float> {

    public MainController controller;
    public GameObject body;
    public Image healthBar;
    //public GameObject healthBG;
    public GameObject healthCanvas;

    public float health;
    public float maxHealth = 10;

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
    public Material nBlue;
    public Material nRed;
    public Material nBlack;

    public bool p1fire;
    public bool p2fire;
    public bool p3fire;
    public bool p3Start = true;

    public bool attacking;

    public bool changeColor;

    // Use this for initialization
    void Start () {

        controller = body.GetComponent<MainController>();
        health = maxHealth;

    }

    

    // Update is called once per frame
    void Update () {

        healthBar.fillAmount = health / maxHealth;

        if(controller.Difficulty <= 1)
        {
            healthCanvas.SetActive(true);
        }
        if(controller.Difficulty >= 2)
        {
            healthCanvas.SetActive(false);
        }

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
                //atkTime = p3Time;
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
        if(controller.attackPhase == controller.maxAttackPhase - 1 && disabled == true)
        {
            dead = true;
            transform.gameObject.tag = "Untagged";
            DestroyPhys();
        }
        if(disabled == true)
        {
            Tbody.GetComponent<MeshRenderer>().material = nBlack;
            cap.GetComponent<MeshRenderer>().material = nBlack;
        }
        if(disabled == false && changeColor == false)
        {
            Tbody.GetComponent<MeshRenderer>().material = nBlue;
            cap.GetComponent<MeshRenderer>().material = nBlue;
        }
        if(changeColor == true)
        {
            Tbody.GetComponent<MeshRenderer>().material = nRed;
            cap.GetComponent<MeshRenderer>().material = nRed;
            Invoke("ChangeBack", .1f);
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
        if(p3Start == true)
        {
            atkTime = p3Time / 3;
            yield return new WaitForSeconds(atkTime);
        }
        if (p3Start == false)
        {
            atkTime = p3Time;
            yield return new WaitForSeconds(atkTime);
        }
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
        if (health > 0)
        {
            changeColor = true;
        }
    }

    public void DestroyPhys()
    {
        Tbody.SetActive(false);
        cap.SetActive(false);
        barrel.SetActive(false);
        spawn.SetActive(false);
        //healthBG.SetActive(false);
    }

    public void ChangeBack()
    {
        Tbody.GetComponent<MeshRenderer>().material = nBlue;
        cap.GetComponent<MeshRenderer>().material = nBlue;
        changeColor = false;
    }
}

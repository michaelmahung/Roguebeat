using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject //Must take the IPooledObject interface because it contains logic for when an object is activated from the object pool.
{
    public float projectileSpeed;
    public float activeTime;
    public bool HitEnemy;
    public bool HitWall;

    public void OnObjectSpawn()
    {
        Invoke("Deactivate", activeTime); //Invoke will simply call a method after a certain amount of time, here it will deactivate old objects after a set time.
    }

    public void FixedUpdate()
    {
        //Projectile should move forwards over time - Using fixedupdate because movement is jittery without it
        //HOWEVER, with FixedUpdate, I should make the movement physics based instead of using Time.deltaTime
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    } 

    public void Deactivate()
    {
        //Set the gameobject to be deactive
        gameObject.SetActive(false);
    }

    void OnCollisionEnter (Collision other)
	{
	if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Wall"){
	gameObject.SetActive(false);
	}
	}
	}
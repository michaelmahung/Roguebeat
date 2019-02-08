using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlamethrower : EnemyProjectile
{

    public float Life = 1.0f;
    public float Speed = 15.0f;
    public GameObject missile;

    private void Start()
    {
        Damage = 1 * GameManager.Instance.Difficulty;
    }

    private void Update()
    {
        Life -= Time.deltaTime;
        if (Life <= 0)
        {
            Destroy(gameObject);
        }
        if (Life > 0)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }
    /*private void OnTriggerEnter(Collision collider)
    {
        if (collider.gameObject.tag == "eProjectile")
        {
            Physics.IgnoreCollision(missile.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }*/


}


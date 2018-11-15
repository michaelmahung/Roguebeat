using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    public float projectileSpeed;
    public float activeTime;

    public void OnObjectSpawn()
    {
        Invoke("Deactivate", activeTime);
    }

    public void FixedUpdate()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    } 

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

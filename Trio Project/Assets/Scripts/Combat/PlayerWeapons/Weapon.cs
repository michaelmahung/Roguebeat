using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject 
{
    public new string name;
    public string description;

    public float damage;
    public float fireRate;
    public float projectileSpeed;

    public AudioClip fireSound;

    public BaseProjectile projectile;

    public GameObject[] projectileSpawnLocations;

    public void Print()
    {
        Debug.Log(name);
    }
}

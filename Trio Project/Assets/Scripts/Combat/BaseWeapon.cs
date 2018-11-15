using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Texture2D))]
public abstract class BaseWeapon: MonoBehaviour
{
    public Player player;
    public string weaponName = "Base Weapon";
    public string projectileName = "Base Projectile";
    private AudioSource audioSource;
    public AudioClip fireSound;
    public float fireRate = 0.5f;
    public float damage = 1;
    public float projectileSpeed = 1;
    public Texture2D icon;
    public bool canFire = true;
    public GameObject projectile;
    public int projectileSpawnAmount = 50;
    public List<GameObject> fireLocations = new List<GameObject>();
    public ProjectilePoolManager.ProjectilePool projectileKey;

    public virtual void Start()
    {
        //projectileSpawnLocations = player.fireLocations;
        audioSource = GetComponent<AudioSource>();
        projectileKey.prefab = projectile;
        projectileKey.size = projectileSpawnAmount;
        projectileKey.tag = projectileName;
        ProjectilePoolManager.Instance.Pools.Add(projectileKey);
    }

    public virtual void Update()
    {

    }

    public virtual void Fire()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(WeaponCooldown());
            audioSource.clip = fireSound;
            audioSource.Play();
            ShootWeapon();
        } else
        {
            //Debug.Log("Cant Fire " + weaponName);
            return;
        }
    }

    public abstract void ShootWeapon();

    public IEnumerator WeaponCooldown()
    {
        //Debug.Log("Firing " + weaponName);
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

}

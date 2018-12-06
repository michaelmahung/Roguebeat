using UnityEngine;
using System.Collections.Generic;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public abstract class BaseWeapon: MonoBehaviour
{
    protected List<GameObject> fireLocations = new List<GameObject>();
    protected string projectileName;
    protected string weaponName;
    protected bool canFire = true;
    protected AudioSource audioSource;

    [Header("Weapon Information")]
    [Range(0.5f, 10)]
    public float weaponDamage = 1;
    [Range(0.05f, 3)]
    public float fireRate = 0.5f;
    [Range(0.01f, 0.5f)]
    public float swapTime = 0.25f;

    [Header("Projectile Information")]
    [Range(5, 150)]
    public int projectileSpawnAmount = 50;
    [Range(1, 60)]
    public int projectileSpeed = 30;
    [Range(0, 10)]
    public float projectileLife = 5;

    [Header("Misc")]
    public GameObject projectile;
    public AudioClip fireSound;
    public Texture2D icon;


    public virtual void Awake()
    {
        weaponName = gameObject.name;
        projectileName = weaponName + " Projectile";
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.15f;

        if (icon == null)
        {
            icon = Resources.Load<Texture2D>("Icons/DefaultIcon");
        }

        if (fireSound == null)
        {
            fireSound = Resources.Load<AudioClip>("ProjectileSounds/DefaultSound");
        }

        foreach (Transform firelocation in transform)
        {
            if (firelocation.name.Contains("FireLocation"))
            {
                fireLocations.Add(firelocation.gameObject);
            }
        }
    }


    public virtual void Start()
    {
        try
        {
            ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
        }
        catch
        {
            if (ProjectilePoolManager.Instance == null)
            {
                GameObject go = new GameObject("ProjectilePoolManager");
                go.AddComponent<ProjectilePoolManager>();
                ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
            }
        }
    }

    public virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    public virtual void Fire()
    {
        if (canFire && !GameManager.Instance.UI.gamePaused)
        {
            canFire = false;
            StartCoroutine(WeaponCooldown());
            audioSource.clip = fireSound;
            audioSource.Play();
            ShootWeapon();
        } 
    }


    public virtual void ShootWeapon()
    {
        for (int i = 0; i < fireLocations.Count; i++)
        {
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation, weaponDamage, projectileSpeed, projectileLife);
        }
    }

    public virtual IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        yield break;
    }

    public virtual IEnumerator SwapCooldown()
    {
        yield return new WaitForSeconds(swapTime);
        canFire = true;
        yield break;
    }

    public virtual void OnEnable()
    {
        audioSource.Stop();
        canFire = false;
        StartCoroutine(SwapCooldown());
    }

    public virtual void OnDisable()
    {
        canFire = false;
        StopAllCoroutines();
    }

}

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
    [Range(0.1f, 10)]
    public float weaponDamage = 1;
    [Range(0.05f, 3)]
    public float fireRate = 0.5f;
    [Range(0.01f, 1)]
    public float swapTime = 0.5f;

    [Header("Projectile Information")]
    [Range(5, 200)]
    public int projectileSpawnAmount = 50;
    [Range(1, 100)]
    public int projectileSpeed = 30;
    [Range(0, 10)]
    public float projectileLife = 5;

    [Header("Misc")]
    public GameObject projectile;
    public AudioClip fireSound;
    public Texture2D icon;

    /*public CombatStructs.WeaponInformation wepInfo;
    public CombatStructs.ProjectileInformation projInfo;*/


    //By making Start(), Update(), and Fire() virtual, the children of this object will have the option of overriding the respective functions. 
    public virtual void Awake()
    {
        weaponName = gameObject.name;
        projectileName = weaponName + " Projectile";
        audioSource = GetComponent<AudioSource>(); //Get the attached AudioSource
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

    //To avoid errors, we need to do some error handling. What this bit does is:
    //Attempt to feed variables to the projectile pool manager instance.
    //If the instance does not exist, create a gameobject and attach an instance to it.
    //Once the instance is created, run the function again.
    //This will make also prevent multiple instances of the pool being created, as the first class to
    //call this will create an instance that the rest of the classes can use.

    public virtual void Start()
    {
        try
        {
            //Attempt to give the ProjectilePoolManager information for a new projectile
            ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
        }
        catch
        {
            //If the ProjectilePoolManager isn't found, create an empty game object and attach one to it.
            //Debug.LogWarning("No ProjectilePoolManager found in scene, Creating one now...");
            if (ProjectilePoolManager.Instance == null)
            {
                GameObject go = new GameObject("ProjectilePoolManager");
                go.AddComponent<ProjectilePoolManager>();
                //Once the new ProjectilePoolManager is in the scene, try to give it the projectile information
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
        if (canFire && !GameManager.Instance.gamePaused)
        {
            //On fire, set fire to false, start the cooldown and play the fire sound, afterwards, run the shoot weapon function.
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
            //Behold, the longest function call of this project - I hope.
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation, weaponDamage, projectileSpeed, projectileLife);
        }
    }


    //Coroutine that handles the weapon cooldown
    public virtual IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        yield break;
    }


    //Coroutine that handles what happens when swapping to this weapon
    public virtual IEnumerator SwapCooldown()
    {
        yield return new WaitForSeconds(swapTime);
        canFire = true;
        yield break;
    }


    //Functions to handle what happens when the weapons are enabled and disabled.
    public virtual void OnEnable()
    {
        audioSource.Stop();
        canFire = false;
        StartCoroutine(SwapCooldown());
    }

    public virtual void OnDisable()
    {
        //audioSource.Stop();
        canFire = false;
        StopAllCoroutines();
    }

}

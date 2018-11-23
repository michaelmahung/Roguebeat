using UnityEngine;
using System.Collections.Generic;
using System.Collections;


//Id rather attach an audiosource to each weapon to allow for simultaneous weapon sounds.
//Making this class abstract will make sure that nobody accidentally attaches it to a new weapon.
[RequireComponent(typeof(AudioSource))]
public abstract class BaseWeapon: MonoBehaviour
{
    protected Player player;

    protected List<GameObject> fireLocations = new List<GameObject>();

    protected string weaponName;

    [Header("Weapon Information")]

    public Texture2D icon;
    [Range(0.1f, 10)]
    public float weaponDamage = 1;
    [Range(0.05f, 3)]
    public float fireRate = 0.5f;
    [Tooltip("How long you have to wait after switching to this weapon to fire")]
    [Range(0.01f, 1)]
    public float swapTime = 0.5f;

    [Header("Projectile Information")]

    public GameObject projectile;
    [Tooltip("How many of this projectile should be spawned into it's respectile object pool")]
    [Range(5, 200)]
    public int projectileSpawnAmount = 50;
    [Range(1, 100)]
    public int projectileSpeed = 30;
    public AudioClip fireSound;

    string projectileName;
    bool canFire = true;
    AudioSource audioSource;


    //By making Start(), Update(), and Fire() virtual, the children of this object will have the option of overriding the respective functions. 
    public virtual void Awake()
    {
        weaponName = gameObject.name;
        audioSource = GetComponent<AudioSource>(); //Get the attached AudioSource
        audioSource.volume = 0.20f;
        projectileName = weaponName + " Projectile"; //Just adds projetile to the end of the weapon name

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
        //To avoid errors, we need to do some error handling. What this bit does is:
        //Attempt to feed variables to the projectile pool manager instance.
        //If the instance does not exist, create a gameobject and attach an instance to it.
        //Once the instance is created, run the function again.
        //This will make also prevent multiple instances of the pool being created, as the first class to
        //call this will create an instance that the rest of the classes can use.
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
        } else
        {
            //Debug.Log("Cant Fire " + weaponName);
            return;
        }
    }


    //By making this function abstract, we ensure that every script that inherits from this base class will have to assign a unique shootweapon function.
    //If the function is not assigned, the script will throw an error and will not compile. 
    public virtual void ShootWeapon()
    {
        for (int i = 0; i < fireLocations.Count; i++)
        {
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation, weaponDamage, projectileSpeed);
        }
    }


    //Coroutine that handles the weapon cooldown
    public IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        yield break;
    }


    //Coroutine that handles what happens when swapping to this weapon
    public IEnumerator SwapCooldown()
    {
        yield return new WaitForSeconds(swapTime);
        canFire = true;
        yield break;
    }


    //Functions to handle what happens when the weapons are enabled and disabled.
    private void OnEnable()
    {
        audioSource.Stop();
        canFire = false;
        StartCoroutine(SwapCooldown());
    }

    private void OnDisable()
    {
        //audioSource.Stop();
        canFire = false;
        StopAllCoroutines();
    }

}

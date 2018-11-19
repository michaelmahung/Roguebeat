using UnityEngine;
using System.Collections.Generic;
using System.Collections;


//Id rather attach an audiosource to each weapon to allow for simultaneous weapon sounds.
//Making this class abstract will make sure that nobody accidentally attaches it to a new weapon.
[RequireComponent(typeof(AudioSource))]
public abstract class BaseWeapon: MonoBehaviour
{
    [Header("Player Reference")]
    [Tooltip("Reference to the Player")]
    public Player player;
    [Tooltip("Where the weapon can fire projectiles from. By default, each weapon is set to fire from all locations simultaneously")]
    public List<GameObject> fireLocations = new List<GameObject>();
    [Header("Weapon Information")]
    [Tooltip("Name of the weapon")]
    public string weaponName = "Base Weapon";
    [Tooltip("The icon associated with this weapon")]
    public Texture2D icon;
    [Tooltip("How much damage this weapon will deal")]
    public float damage = 1;
    [Tooltip("How often this weapon can fire")]
    [Range(0, 3)]
    public float fireRate = 0.5f;
    [Tooltip("How long you have to wait after switching to this weapon to fire")]
    [Range(0, 1)]
    public float swapTime = 0.5f;
    [Header("Projectile Information")]
    [Tooltip("The projectile to be fired")]
    public GameObject projectile;
    [Tooltip("Name of the weapon projectile, this does not need to be configured \n ***WILL THROW AN ERROR IF THERE ARE DUPLICATE NAMES***")]
    public string projectileName;
    [Tooltip("How many of this projectile should be spawned into it's respectile object pool")]
    [Range(20, 300)]
    public int projectileSpawnAmount = 100;
    [Tooltip("The sound the projectile makes when firing")]
    public AudioClip fireSound;
    [Tooltip("The speed at which this weapon's projectile(s) will move")]
    [Range(10, 75)]
    public float projectileSpeed = 1;
    private bool canFire = true;
    private AudioSource audioSource;


    //By making Start(), Update(), and Fire() virtual, the children of this object will have the option of overriding the respective functions. 
    public virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>(); //Get the attached AudioSource
        projectileName = weaponName + " Projectile"; //Just adds projetile to the end of the weapon name
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
            Debug.LogWarning("No ProjectilePoolManager found in scene, Creating one now...");
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
    public abstract void ShootWeapon();


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

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
    [Tooltip("The key that will be fed to the ProjectilePoolManager for the sake of spawning objects, this does not need to be configured.")]
    public ProjectilePoolManager.ProjectilePool projectileKey;
    private AudioSource audioSource;


    //By making Start(), Update(), and Fire() virtual, the children of this object will have the option of overriding the respective functions. 
    public virtual void Awake()
    {
        //projectileSpawnLocations = player.fireLocations;
        audioSource = GetComponent<AudioSource>();
        projectileName = weaponName + " Projectile";
        projectileKey.prefab = projectile;
        projectileKey.size = projectileSpawnAmount;
        projectileKey.tag = projectileName;
        try
        {
            Debug.Log("Feeding " + projectileName + " information to ProjectilePoolManager");
            ProjectilePoolManager.Instance.ProjectileTypes.Add(projectileKey);
        }
        catch
        {
            Debug.LogWarning("No ProjectilePoolManager found in scene, please add one.");
        }

    }

    public virtual void Start()
    {

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

    public IEnumerator WeaponCooldown()
    {
        //Debug.Log("Firing " + weaponName);
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

}

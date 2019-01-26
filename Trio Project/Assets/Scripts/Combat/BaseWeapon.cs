using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public abstract class BaseWeapon: MonoBehaviour //Another abstract class, we don't want instances of this class in the game, only children of this class.
{
    protected List<GameObject> fireLocations = new List<GameObject>();
    protected string projectileName;
    protected string weaponName;
    protected bool canFire = true;

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
    public bool WeaponActive
    {
        get { return weaponActive; }
        private set { weaponActive = value; }
    }
    public int WeaponCost
    {
        get { return weaponCost; }
        private set { weaponCost = value; }
    }

    [SerializeField]
    private bool weaponActive;
    [SerializeField]
    private int weaponCost;


    public virtual void Awake()
    {
        weaponName = gameObject.name;

        if (!DataManager.HasPref(weaponName))
        {
            SetWeaponActive(true);
        }

        projectileName = weaponName + " Projectile";

        if (icon == null)
        {
            icon = Resources.Load<Texture2D>("Icons/DefaultIcon");
        }

        if (fireSound == null)
        {
            fireSound = Resources.Load<AudioClip>("ProjectileSounds/DefaultSound");
        }

        //For every child object of this GameObject that has the word FireLocation in it's name..
        //I need to change this probably btw, this sucks.
        foreach (Transform firelocation in transform)
        {
            if (firelocation.name.Contains("FireLocation"))
            {
                //Add the objects location to our fire locations.
                fireLocations.Add(firelocation.gameObject);
            }
        }
    }


    public virtual void Start()
    {
        //try / catch statements are just saying, I want to try doing this and if there are errors do what's in the catch segment. 
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
        if (canFire && !GameManager.Instance.UI.GamePaused)
        {
            canFire = false;
            StartCoroutine(WeaponCooldown());
            AudioManager.Instance.PlaySound(fireSound.name);
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

    //May switch these to update as they create a little bit of garbage
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
        canFire = false;
        StartCoroutine(SwapCooldown());
    }

    public virtual void OnDisable()
    {
        canFire = false;
        StopAllCoroutines();
    }

    public void SetWeaponActive(bool value)
    {
        WeaponActive = value;
        DataManager.SetPref(weaponName, value);
    }

    public void GetWeaponActive()
    {
        if (DataManager.HasPref(weaponName))
        {
            WeaponActive = DataManager.GetPref(weaponName);
        }
    }

}

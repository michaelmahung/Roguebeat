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
    [Range(0.5f, 20)]
    [SerializeField] protected float weaponDamage = 1;
    [Range(0.05f, 3)]
    [SerializeField] protected float fireRate = 0.5f;
    [Range(0.01f, 0.5f)]
    [SerializeField] protected float swapTime = 0.25f;
    [Range(0, 800)]
    [SerializeField] protected int RecoilAmount = 50;
    [Range(2, 20)]
    [SerializeField] protected float ScreenShakeAmount = 8f;
    [Range(0.01f, .5f)]
    [SerializeField] protected float ScreenShakeDuration = 0.15f;

    [Header("Projectile Information")]
    [Range(5, 150)]
    [SerializeField] protected int projectileSpawnAmount = 50;
    [Range(1, 60)]
    [SerializeField] protected int projectileSpeed = 30;
    [Range(0, 10)]
    [SerializeField] protected float projectileLife = 5;

    [Header("Misc")]
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected AudioClip fireSound;
    [SerializeField] protected Texture2D icon;

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
    protected bool weaponActive;
    [SerializeField]
    protected int weaponCost;


    public virtual void Awake()
    {
        weaponName = gameObject.name;
        projectileName = weaponName + " Projectile";

        SetWeaponActive(true);

        //Automatically setting weapons active while testing
        //TODO make sure all but base weapon will be disabled for demo
        if (!DataManager.HasPref(weaponName))
        {
            SetWeaponActive(true);
        }

        //If no unique icon is set, simply load the default icon
        if (icon == null)
        {
            icon = Resources.Load<Texture2D>("Icons/DefaultIcon");
        }

        //If no unique fire sound it set, load the default fire sound
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
        //try / catch statements are just saying, I want to try doing this thing - if there are errors while trying to do it, do what's in the catch segment. 
        try
        {
            //Try adding our projectile to the projectile dictionary
            ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
        }
        catch
        {
            //If there is no projectile dictionary, create one and add our projectile to it.
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
            SFXManager.Instance.PlaySound(fireSound.name);
            ShootWeapon();
        } 
    }


    public virtual void ShootWeapon()
    {
        //All this is doing is positioning and spawning a projectile at each fire location
        for (int i = 0; i < fireLocations.Count; i++)
        {
            //Boo this function, need to refactor - Spawn an object from the pool and pass the weapons values down to the projectile.
            ProjectilePoolManager.Instance.SpawnFromPool(projectileName, fireLocations[i].transform.position, fireLocations[i].transform.rotation, weaponDamage, projectileSpeed, projectileLife);
        }

        GameManager.Instance.PlayerMovementReference.PushBackPlayer(RecoilAmount);
        GameManager.Instance.CameraShaker.ShakeMe(ScreenShakeAmount, ScreenShakeDuration);
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

    public bool GetWeaponActive()
    {
        if (DataManager.HasPref(weaponName))
        {
            WeaponActive = DataManager.GetPref(weaponName);
        }

        return DataManager.GetPref(weaponName);
    }

}

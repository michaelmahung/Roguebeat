using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public abstract class BaseWeapon: MonoBehaviour //Another abstract class, we don't want instances of this class in the game, only children of this class.
{
    protected List<GameObject> fireLocations = new List<GameObject>();
    protected string projectileName;
    protected string weaponName;
    protected bool canFire = true;

    [SerializeField] protected ProjectileTypes ProjectileKey;
    [Header("Weapon Information")]
    [Range(0.5f, 20)]
    [SerializeField] protected float weaponDamage = 1;
    [Range(0.05f, 3)]
    [SerializeField] protected float fireRate = 0.5f;
    [Range(0.01f, 0.5f)]
    [SerializeField] protected float swapTime = 0.25f;
    [Range(0, 800)]
    [SerializeField] protected int RecoilAmount = 50;
    [Range(0, 500)]
    [SerializeField] protected float ScreenShakeAmount = 20f;
    [Range(0.01f, .75f)]
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
    [SerializeField] protected Texture2D icon;
    [SerializeField] protected bool weaponActive;
    [SerializeField] protected int weaponCost;
    [SerializeField] protected AudioInfo fireSoundInfo;

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

    protected float fireTimer;
    protected float swapTimer;

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
        if (fireSoundInfo.clip == null)
        {
            fireSoundInfo.clip = Resources.Load<AudioClip>("ProjectileSounds/DefaultSound");
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

        /*//try / catch statements are just saying, I want to try doing this thing - if there are errors while trying to do it, do what's in the catch segment. 
        try
        {
            //Try adding our projectile to the projectile dictionary
            ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
        }
        catch
        {
            Debug.LogWarning("Generating Projectile Pool Manager");
            //If there is no projectile dictionary, create one and add our projectile to it.
            if (ProjectilePoolManager.Instance == null)
            {
                GameObject go = new GameObject("ProjectilePoolManager");
                go.AddComponent<ProjectilePoolManager>();
                ProjectilePoolManager.Instance.AddProjectileToDictionary(projectileName, projectile, projectileSpawnAmount);
            }
        }*/
    }


    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }

        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
            canFire = false;
        } else
        {
            canFire = true;
        }

        if (swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
            canFire = false;
        }
    }

    public virtual void Fire()
    {
        if (canFire && !GameManager.Instance.UI.GamePaused)
        {
            fireTimer = fireRate;
            canFire = false;
            GameManager.Instance.WeaponSounds.PlayClip(fireSoundInfo);
            ShootWeapon();
        } 
    }


    public virtual void ShootWeapon()
    {
        //All this is doing is positioning and spawning a projectile at each fire location
        for (int i = 0; i < fireLocations.Count; i++)
        {
            ProjectileProperties proj = NewProjectilePooler.Instance.GrabProjectile(ProjectileKey);
            proj.Prefab.transform.position = fireLocations[i].transform.position;
            proj.Prefab.transform.rotation = fireLocations[i].transform.rotation;
            proj.PrefabBehaviour.ProjectileDamage = weaponDamage;
            proj.PrefabBehaviour.ProjectileSpeed = projectileSpeed;
            proj.PrefabBehaviour.ActiveTime = projectileLife;
            proj.Prefab.SetActive(true);
        }

        GameManager.Instance.PlayerMovementReference.PushBackPlayer(RecoilAmount);
        GameManager.Instance.CameraShaker.CustomShake(ScreenShakeDuration, ScreenShakeAmount);
    }

    public virtual void OnEnable()
    {
        canFire = false;
        swapTimer = swapTime;
    }

    public virtual void OnDisable()
    {
        canFire = false;
        swapTimer = swapTime;
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

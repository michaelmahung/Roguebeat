using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : BaseWeapon 
{
    private static TriShot _instance;
    public static TriShot Instance { get { return _instance; } }

    [SerializeField] private float heatLimit = 5;
    [SerializeField] private List<EmissionColor> myEmissions;
    [SerializeField] private float _currentHeat;
    [SerializeField] private float emissionTimer = 1;
    private bool overheated;
    private float currentHeat { get { return _currentHeat; } set { if (value <= 0) { value = 0; } _currentHeat = value; } }
    private float overheatPercent { get { return currentHeat / heatLimit; } }
    bool holdingFire;

    new void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        foreach (GameObject ob in fireLocations)
        {
            myEmissions.Add(ob.GetComponent<EmissionColor>());
        }

        base.Awake();
    }

    public override void Update()
    {
        base.Update();

        if (emissionTimer > 1)
        {
            emissionTimer = 0;
            SetColor();
        }

        emissionTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && !overheated)
        {
            holdingFire = true;
            currentHeat += Time.deltaTime;
            
            if (canFire)
            {
                Fire();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdingFire = false;
        }

        if (currentHeat >= heatLimit)
        {
            overheated = true;
            canFire = false;
        }

        if (!holdingFire)
        {
            if (currentHeat > 0)
            {
                if (overheated)
                {
                    canFire = false;
                    currentHeat -= Time.deltaTime * 0.75f;
                }
                else
                {
                    currentHeat -= Time.deltaTime;
                }

                if (currentHeat == 0 && overheated)
                {
                    canFire = true;
                    overheated = false;
                }
            }
        }
    }

    void SetColor()
    {
        foreach (EmissionColor emis in myEmissions)
        {
            emis.SetColor(overheatPercent);
        }
    }

    //Nothing to see here
    new void Start()
    {
        base.Start();
    }
}

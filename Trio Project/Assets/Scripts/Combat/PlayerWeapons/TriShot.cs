using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShot : BaseWeapon 
{
    private static TriShot _instance;
    public static TriShot Instance { get { return _instance; } }

    [SerializeField] private float heatLimit = 5;
    [SerializeField] private float _currentHeat;
    [SerializeField] private bool overheated;
    [SerializeField] private List<EmissionColor> myEmissions;
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

        base.Awake();
        
        foreach(GameObject ob in fireLocations)
        {
            myEmissions.Add(ob.GetComponent<EmissionColor>());
        }

    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) && !overheated)
        {
            holdingFire = true;
            currentHeat += Time.deltaTime;
            //Debug.Log(currentHeat);
            Fire();
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdingFire = false;
        }

        if (!holdingFire)
        {
            if (currentHeat > 0)
            {
                if (overheated)
                {
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

        if (currentHeat >= heatLimit)
        {
            overheated = true;
            canFire = false;
        }

        foreach (EmissionColor emis in myEmissions)
        {
            //Debug.Log(overheatPercent);
            emis.SetColor(overheatPercent);
        }
    }

    //Nothing to see here
    new void Start()
    {
        base.Start();
    }
}

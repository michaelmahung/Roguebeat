using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireStates
{
    Idle,
    Firing,
    Reloading
}

//Similar to the tiles, each weapon will handle its own states
//I need to create an update loop that will check the attack speed fed in, and fire + reload within that time frame.
//Doing so will allow me to change the main controller fire speed, and each weapon will follow suit

public class TileBossWeapon : MonoBehaviour
{
    [SerializeField] TileBossWeaponController controller;
    FireStates currentState;

    public void Fire(float speed)
    {
        currentState = FireStates.Firing;
    }

    void Reload()
    {
        currentState = FireStates.Reloading;
    }



    private void Update()
    {
        switch (currentState)
        {
            case FireStates.Idle:
                break;
            case FireStates.Firing:
                break;
            case FireStates.Reloading:
                break;
            default:
                break;
        }
    }
}

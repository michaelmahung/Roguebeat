using UnityEngine;
using System.Collections.Generic;


public class WeaponSwitching : MonoBehaviour
{
    public BaseWeapon[] allWeapons { get; private set; }
    private int weaponIndex;
    private PlayerWeapon playerWeapon;
    IWeaponSwap weaponSwap;

    public static WeaponSwitching Instance;

    void Start()
    {
        Instance = this;
        playerWeapon = GameManager.Instance.Player.GetComponent<PlayerWeapon>();
        allWeapons = transform.GetComponentsInChildren<BaseWeapon>();
        weaponSwap = GameManager.Instance.UI.GetComponent<IWeaponSwap>();

        CheckWeapons();
    }

    private void CheckWeapons()
    {
        foreach (BaseWeapon weapon in allWeapons)
        {
            weapon.gameObject.SetActive(false);
        }

        weaponIndex = 0;
        allWeapons[weaponIndex].gameObject.SetActive(true);
        UpdateWeapon();
    }

    //Take in the current weapon and use that as a starting point
    private void NextWeapon(int startingIndex)
    {
        //Go to the weapon immediately after the weapon we started with (to avoid checking for the weapon we already have equipped.)
        for (int i = startingIndex + 1; i < allWeapons.Length + 1; i++)
        {
            //If we're at the end of our array, start back at 0
            if (i == allWeapons.Length)
            {
                i = 0;
            }

            //If we have ended up back where we started, do nothing.
            if (i == startingIndex)
            {
                break;
            }
            //Otherwise, switch to the next active weapon found.
            else if (allWeapons[i].WeaponActive)
            {
                weaponIndex = i;
                allWeapons[startingIndex].gameObject.SetActive(false);
                allWeapons[weaponIndex].gameObject.SetActive(true);
                UpdateWeapon();
                break;
            } else
            {
                //Redundant, but if we're not at the weapon we started at, and the weapon we're at isnt active, just keep going through the loop.
                continue;
            }
        }
    }

    void UpdateWeapon()
    {
        playerWeapon.playerWeapon = allWeapons[weaponIndex].gameObject;
        weaponSwap.WeaponSwapped();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            NextWeapon(weaponIndex);
        }
    }
}

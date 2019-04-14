using UnityEngine;
using System.Collections.Generic;


public class WeaponSwitching : MonoBehaviour
{
    private BaseWeapon[] allWeapons;
    private int weaponIndex;
    private PlayerWeapon playerWeapon;
    IWeaponSwap weaponSwap;

    public static WeaponSwitching Instance;

    void Start()
    {
        Instance = this;
        playerWeapon = GameManager.Instance.PlayerObject.GetComponent<PlayerWeapon>();
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

    //We are going to check our weapons starting at the weapon immediately after the one we are using.
    //If we loop through the entire array of weapons and none of them can be activated, don't do anything.
    private void NextWeapon(int startingIndex)
    {
        for (int i = startingIndex + 1; i < allWeapons.Length + 1; i++)
        {
            if (i == allWeapons.Length)
            {
                i = 0;
            }

            if (i == startingIndex)
            {
                break;
            } else if (allWeapons[i].WeaponActive)
            {
                weaponIndex = i;
                allWeapons[startingIndex].gameObject.SetActive(false);
                allWeapons[weaponIndex].gameObject.SetActive(true);
                UpdateWeapon();
                break;
            } else
            {
                continue;
            }
        }
    }

    /*private void NextWeapon(int currentIndex)
    {
        Debug.Log(currentIndex);

        if (currentIndex < allWeapons.Length - 1)
        {
            weaponIndex += 1;
        } else
        {
            weaponIndex = 0;
        }

        if (allWeapons[weaponIndex].WeaponActive)
        {
            allWeapons[currentIndex].gameObject.SetActive(false);
            allWeapons[weaponIndex].gameObject.SetActive(true);
            UpdateWeapon();
        } else
        {
            allWeapons[currentIndex].gameObject.SetActive(false);
            NextWeapon(weaponIndex); //RECURSION LETS GO
        }
    }*/

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

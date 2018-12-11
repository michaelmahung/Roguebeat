using UnityEngine;
using System.Collections.Generic;


//Honestly this is a trash system and I saw it in a video and decided to copy it.

public class WeaponSwitching : MonoBehaviour
{
    private PlayerWeapon playerWeapon;
    private List<GameObject> weaponList = new List<GameObject>();
    private int currentWeapon;
    bool weaponsAdded;

    public static WeaponSwitching Instance;

    void Start()
    {
        Instance = this;
        playerWeapon = GameManager.Instance.Player.GetComponent<PlayerWeapon>();
        SelectWeapon();
        UpdateWeapon();
    }


    void Update()
    {
        //Why even do this
        int previousWeapon = currentWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (currentWeapon >= transform.childCount -1)
            {
                currentWeapon = 0;
            }else
            {
                //If were not at the last weapon, go to the next one and update the UI.
                currentWeapon++;
            }
            UpdateWeapon();
        }

        if (previousWeapon != currentWeapon)
        {
            SelectWeapon();
        }
    }

    //Update weapon tells the playerWeapon component on the player what weapon we are using and updates the UI for some reason.
    public void UpdateWeapon()
    {
        playerWeapon.playerWeapon = weaponList[currentWeapon];

        try
        {
            //Try to update the weapon in the UI component.
            //If there are any errors in doing so, just stop.
            IWeaponSwap weaponSwap = GameManager.Instance.UI.GetComponent<IWeaponSwap>();
            weaponSwap.WeaponSwapped();
        }
        catch
        {
            return;
            //Debug.LogError("No UIController script found in scene");
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (!weaponsAdded)
            {
                //If we havent made our list of weapons, do that and dont let it do it again.
                weaponList.Add(weapon.gameObject);
            }

            if (i == currentWeapon)
            {
                //If our weapon index is the selected weapon's index, activate it, otherwise deactivate it
                weapon.gameObject.SetActive(true);
            }else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
        weaponsAdded = true;
    }

}

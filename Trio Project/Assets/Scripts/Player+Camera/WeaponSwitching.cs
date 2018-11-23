using UnityEngine;
using System.Collections.Generic;

public class WeaponSwitching : MonoBehaviour
{

    public int currentWeapon;
    public List<GameObject> weaponList = new List<GameObject>();
    bool weaponsAdded;
    public Player player;

    public static WeaponSwitching Instance; //For the sake of error handling, well make an instance of this.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Instance = this;
        SelectWeapon(); //Running this once at the beginning will deactive all but the first weapon.
        weaponsAdded = true; //The weapons have been added to the list.
        UpdateWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon; //After switching weapons, we need to keep track of what the last weapon is.

        if (Input.GetKeyDown(KeyCode.Alpha3)) //When 3 is pressed
        {
            if (currentWeapon >= transform.childCount -1)
            {
                currentWeapon = 0; //If we are at the last item of the list, reset it.
            }else
            {
                currentWeapon++; //Otherwise, increment the current weapon value by 1.
            }
            UpdateWeapon();
        }

        if (previousWeapon != currentWeapon) //If we are actually switching to a new weapon
        {
            SelectWeapon(); //Do this
        }
    }

    public void UpdateWeapon()
    {
        player.playerWeapon = weaponList[currentWeapon];

        try
        {
            IWeaponSwap weaponSwap = GameManager.Instance.UI.GetComponent<IWeaponSwap>();
            weaponSwap.WeaponSwapped();
        }
        catch
        {
            Debug.LogError("No UIController script found in scene");
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform) //For each child object attached to this gameobject.
        {
            if (!weaponsAdded) //If we havent added the current weapon to our weapon list
            {
                weaponList.Add(weapon.gameObject); //Add the current weapon to our weapon list. 
            }
            if (i == currentWeapon) //If the current weapon index matches our weapon.
            {
                weapon.gameObject.SetActive(true); //Activate the weapon
            }else
            {
                weapon.gameObject.SetActive(false); //Otherwise deactivate the weapon
            }

            i++;
        }
        weaponsAdded = true;
    }

}

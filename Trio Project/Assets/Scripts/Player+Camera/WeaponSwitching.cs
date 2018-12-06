using UnityEngine;
using System.Collections.Generic;

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
        int previousWeapon = currentWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (currentWeapon >= transform.childCount -1)
            {
                currentWeapon = 0;
            }else
            {
                currentWeapon++;
            }
            UpdateWeapon();
        }

        if (previousWeapon != currentWeapon)
        {
            SelectWeapon();
        }
    }

    public void UpdateWeapon()
    {
        playerWeapon.playerWeapon = weaponList[currentWeapon];

        try
        {
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
                weaponList.Add(weapon.gameObject);
            }
            if (i == currentWeapon)
            {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBossWeaponController : MonoBehaviour
{
    [SerializeField] TileBossWeapon[] Phase1Weapons;
    [SerializeField] TileBossWeapon[] Phase2Weapons;
    [SerializeField] TileBossWeapon[] Phase3Weapons;
    bool active;
    TileBossWeapon currentWeapon;
    TileBossWeapon previousWeapon;

    public void SetValues()
    {
        currentWeapon = null;
        previousWeapon = null;
    }

    private void Update()
    {
        if (active)
        {

        }
    }

    public void Attack(BossStates state, float attackSpeed)
    {
        switch (state)
        {
            case BossStates.Phase1:
                SelectNewWeapon(Phase1Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossStates.Phase2:
                SelectNewWeapon(Phase2Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossStates.Phase3:
                SelectNewWeapon(Phase3Weapons);
                FireWeapon(attackSpeed);
                break;
            case BossStates.Default:
                break;
            default:
                break;
        }
    }

    void SelectNewWeapon(TileBossWeapon[] phase)
    {
        int randomInt = Random.Range(0, phase.Length);

        previousWeapon = currentWeapon;
        currentWeapon = phase[randomInt];
    }

    void FireWeapon(float _speed)
    {
        currentWeapon.Fire(_speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public HomingLauncher homingLauncher;
    public Weapon[] weapons;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpgradeWeapon(int weaponIndex)
    {
        weapons[weaponIndex].WeaponUpGrade();
    }
}

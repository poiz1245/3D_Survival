using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{

    public void SowrdUp()
    {
        WeaponManager.instance.UpgradeWeapon(0);
    }
    public void AoEUp()
    {
        WeaponManager.instance.UpgradeWeapon(1);
    }
    public void HomingLaucherUp()
    {
        WeaponManager.instance.UpgradeWeapon(2);
    }
    public void MissileUp()
    {
        WeaponManager.instance.UpgradeWeapon(3);
    }
   
    public void PlayerMaxHpUp()
    {
        GameManager.Instance.player.maxHp *= 1.3f;
    }
    public void PlayerPowerUp()
    {
        GameManager.Instance.player.playerAttackPower *= 1.1f;
    }
    public void PlayerShieldUp()
    {
        GameManager.Instance.player.playerShield *= 1.1f;
    }
    public void PlayerScanRangeUp()
    {
        GameManager.Instance.player.objectScanRadius *= 1.3f;
    }
}

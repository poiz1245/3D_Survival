using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{
    public void SowrdUp()
    {
        Time.timeScale = 1.0f;
        WeaponManager.instance.UpgradeWeapon(0);
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void AoEUp()
    {
        Time.timeScale = 1.0f;
        WeaponManager.instance.UpgradeWeapon(1);
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void HomingLaucherUp()
    {
        Time.timeScale = 1.0f;
        WeaponManager.instance.UpgradeWeapon(2);
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void MissileUp()
    {
        Time.timeScale = 1.0f;
        WeaponManager.instance.UpgradeWeapon(3);
        UIManager.Instance.buttonPanel.SetActive(false);
    }
   
    public void PlayerMaxHpUp()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.maxHp *= 1.3f;
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void PlayerPowerUp()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.playerAttackPower *= 1.1f;
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void PlayerShieldUp()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.playerShield *= 1.1f;
        UIManager.Instance.buttonPanel.SetActive(false);
    }
    public void PlayerScanRangeUp()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.objectScanRadius *= 1.3f;
        UIManager.Instance.buttonPanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{

    public void SowrdUp()
    {
        WeaponManager.instance.UpgradeWeapon(0);
        //level1 : 스폰
        //level2 : 스폰
        //level3 : 회전스피드 증가
        //level4 : 크기 증가
    }
    public void AoEUp()
    {
        //level1 : 소환
        //level2 : 범위 증가
        //level4 : 데미지 증가
        //level3 : 데미지 쿨타임 감소
    }
    public void MissileUp()
    {
        //level1 : 공속 증가
        //level2 : 발사 방향 추가
        //level3 : 데미지 증가
        //level4 : 발사 방향 추가
    }
    public void HomingLaucherUp()
    {
        //level1 : 소환
        //level2 : 발사체 증가
        //level3 : 발사 속도 증가
        //level4 : 데미지 증가
    }
    public void PlayerMaxHpUp()
    {
        GameManager.Instance.player.maxHp += 50;
    }
    public void PlayerPowerUp()
    {
        GameManager.Instance.player.playerAttackPower += 10;
    }
    public void PlayerShieldUp()
    {
        GameManager.Instance.player.playerShield += 10;
    }
    public void PlayerScanRangeUp()
    {
        GameManager.Instance.player.objectScanRadius += 2;
    }
}

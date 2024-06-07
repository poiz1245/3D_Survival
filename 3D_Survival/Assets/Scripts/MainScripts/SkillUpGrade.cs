using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{

    public void SowrdUp()
    {
        //스폰 갯수 하나씩 증가시키기
    }
    public void AoEUp()
    {
        //범위증가
        //딜타임감소
        //데미지증가
        //몬스터 슬로우
    }
    public void MissileUp()
    {

    }
    public void HomingLaucherUp()
    {

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

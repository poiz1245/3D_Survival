using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{

    public void SowrdUp()
    {
        //���� ���� �ϳ��� ������Ű��
    }
    public void AoEUp()
    {
        //��������
        //��Ÿ�Ӱ���
        //����������
        //���� ���ο�
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

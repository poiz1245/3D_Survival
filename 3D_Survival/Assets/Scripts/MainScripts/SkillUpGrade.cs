using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpGrade : MonoBehaviour
{

    public void SowrdUp()
    {
        WeaponManager.instance.UpgradeWeapon(0);
        //level1 : ����
        //level2 : ����
        //level3 : ȸ�����ǵ� ����
        //level4 : ũ�� ����
    }
    public void AoEUp()
    {
        //level1 : ��ȯ
        //level2 : ���� ����
        //level4 : ������ ����
        //level3 : ������ ��Ÿ�� ����
    }
    public void MissileUp()
    {
        //level1 : ���� ����
        //level2 : �߻� ���� �߰�
        //level3 : ������ ����
        //level4 : �߻� ���� �߰�
    }
    public void HomingLaucherUp()
    {
        //level1 : ��ȯ
        //level2 : �߻�ü ����
        //level3 : �߻� �ӵ� ����
        //level4 : ������ ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] GameObject swordLevel1;
    [SerializeField] GameObject swordLevel2;
    public Sword(int level, float speed, float damage, float range) : base(level, speed, damage, range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = GameManager.Instance.player.playerAttackPower * 0.5f + damage;
        this.range = range;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 1 * speed, 0));
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        if (level == 1)
        {
            //sword ��ȯ
            swordLevel1.SetActive(true);
        }
        else if (level == 2)
        {
            //sword�߰� ��ȯ
            swordLevel2.SetActive(true);
        }
        else if (level == 3)
        {
            //ȸ�����ǵ� 2������
            speed *= 2;
        }
        else if (level == 4)
        {
            //ũ������
            range *= 2;
            gameObject.transform.localScale *= range;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            MeleeMonster meleeMonster = other.gameObject.GetComponent<MeleeMonster>();
            if (meleeMonster != null)
            {
                meleeMonster.GetDamage(damage);
            }
            RangedMonster monsterRanged = other.gameObject.GetComponent<RangedMonster>();
            if (monsterRanged != null)
            {
                monsterRanged.GetDamage(damage);
            }
        }
    }
}

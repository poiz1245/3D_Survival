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
        this.damage = damage;
        this.range = range;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 1 * speed, 0));
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        print(gameObject.name + "���׷��̵� �Ϸ� Level : " + level);

        if (level == 1)
        {
            swordLevel1.SetActive(true);
            UIManager.Instance.SetText(2, "�߰��� ���⸦ ȹ���մϴ�.");
        }
        else if (level == 2)
        {
            swordLevel2.SetActive(true);
            UIManager.Instance.SetText(2, "�� ������ ���� �����մϴ�.");
        }
        else if (level == 3)
        {
            speed *= 2;
            UIManager.Instance.SetText(2, "�� ���� ������ ���� �����մϴ�.");
        }
        else if (level == 4)
        {
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
            BossMonster bossMonster = other.gameObject.GetComponent<BossMonster>();
            if (bossMonster != null)
            {
                bossMonster.GetDamage(damage);
            }
        }
    }
}

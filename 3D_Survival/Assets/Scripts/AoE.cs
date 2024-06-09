using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : Weapon
{
    [SerializeField] float coolTime;
    [SerializeField] ParticleSystem effact;

    LayerMask targetLayer;

    public AoE(int level, float speed, float damage, float range, float coolTime) : base(level, speed, damage, range)
    {
        this.level = level;
        this.range = range;
        this.damage = damage;
        this.coolTime = coolTime;
    }
    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        if (level == 1)
        {
            //소환
            gameObject.SetActive(true);
        }
        else if (level == 2)
        {
            //범위증가
            range *= 1.5f;
            effact.transform.localScale *= range;
        }
        else if (level == 3)
        {
            //데미지증가
            damage *= 2f;
        }
        else if (level == 4)
        {
            //쿨타임감소
            var mainModule = effact.main;
            coolTime *= 0.5f;
            mainModule.duration *= coolTime;

        }
    }
    private void Start()
    {
        InvokeRepeating("Attack", 0f, coolTime);
        targetLayer = LayerMask.GetMask("Monster");
    }

    void Attack()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        foreach (Collider target in targets)
        {
            Monster monsterScript = target.GetComponent<Monster>();
            monsterScript.GetDamage(damage);
        }
    }
}

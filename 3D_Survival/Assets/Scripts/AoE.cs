using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : Weapon
{
    public float coolTime;

    [SerializeField] GameObject particle;

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

        float increasedAmount = range;
        Vector3 particleScale = particle.transform.localScale;
        particle.transform.localScale += particleScale * increasedAmount; // 1.5πË ¡ı∞°
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

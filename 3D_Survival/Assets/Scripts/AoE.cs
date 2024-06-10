using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HomingLauncher;

public class AoE : Weapon
{
    [SerializeField] float coolTime;
    [SerializeField] ParticleSystem effact;

    LayerMask targetLayer;

    public delegate void ObjectVisibilityChangedHandler(bool isSpawn);
    public event ObjectVisibilityChangedHandler OnObjectVisibilityChanged;

    public AoE(int level, float speed, float damage, float range, float coolTime) : base(level, speed, damage, range)
    {
        this.level = level;
        this.range = range;
        this.damage = damage;
        this.coolTime = coolTime;
    }
   
    private void Start()
    {
        targetLayer = LayerMask.GetMask("Monster");
        OnObjectVisibilityChanged += HandleVisibilityChanged;
    }
    void OnVisibilityChanged(bool isVisible)
    {
        OnObjectVisibilityChanged?.Invoke(isVisible);
    }

    private void HandleVisibilityChanged(bool isSpawn)
    {
        effact.gameObject.SetActive(true);
        InvokeRepeating("Attack", 0f, coolTime);
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

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        if (level == 1)
        {
            //��ȯ
            OnVisibilityChanged(true);
        }
        else if (level == 2)
        {
            //��������
            range *= 1.5f;
            effact.transform.localScale *= range;
        }
        else if (level == 3)
        {
            //����������
            damage *= 2f;
        }
        else if (level == 4)
        {
            //��Ÿ�Ӱ���
            var mainModule = effact.main;
            coolTime *= 0.5f;
            mainModule.duration *= coolTime;

        }
    }
}
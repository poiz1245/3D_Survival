using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AoE : Weapon
{
    [SerializeField] float coolTime;
    [SerializeField] ParticleSystem particleSystem;

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
        particleSystem.gameObject.SetActive(true);
        InvokeRepeating("Attack", 3, coolTime);
    }

    void Attack()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        foreach (Collider target in targets)
        {
            MeleeMonster meleeMonster = target.GetComponent<MeleeMonster>();
            RangedMonster rangedMonster = target.GetComponent<RangedMonster>();
            if (meleeMonster != null)
            {
                meleeMonster.GetDamage(damage);
            }
            if(rangedMonster != null)
            {
                rangedMonster.GetDamage(damage);
            }
        }
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        if (level == 1)
        {
            //소환
            OnVisibilityChanged(true);
        }
        else if (level == 2)
        {
            //범위증가
            range *= 1.5f;
            particleSystem.transform.localScale *= range;
        }
        else if (level == 3)
        {
            //데미지증가
            damage *= 2f;
        }
        else if (level == 4)
        {
            //쿨타임감소
            var mainModule = particleSystem.main;
            coolTime *= 0.5f;
            mainModule.duration *= coolTime;

        }
    }
}

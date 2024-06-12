using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AoE : Weapon
{
    [SerializeField] float coolTime;
    [SerializeField] ParticleSystem myParticleSystem;

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
        myParticleSystem.gameObject.SetActive(true);
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
        print(gameObject.name + "무기 업그레이드 완료 Level : " + level);

        if (level == 1)
        {
            OnVisibilityChanged(true);
            UIManager.Instance.SetText(2, "더 넓은 범위의 적을 공격합니다.");
        }
        else if (level == 2)
        {
            range *= 1.2f;
            myParticleSystem.gameObject.transform.localScale *= 1.2f;
            UIManager.Instance.SetText(2, "더 강하게 적을 공격합니다.");
        }
        else if (level == 3)
        {
            damage *= 2f;
            //레벨4가 될 때 어떻게 업그레이들 할 지 다시 정해야함
        }
        else if (level == 4)
        {
            myParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var mainModule = myParticleSystem.GetComponent<ParticleSystem>().main;
            coolTime *= 0.5f;
            mainModule.duration *= coolTime;
            myParticleSystem.Play();

        }
    }
}

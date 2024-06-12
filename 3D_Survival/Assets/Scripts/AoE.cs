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
                if (level == 4)
                {
                    meleeMonster.moveSpeed *= 0.5f;
                }
            }
            if (rangedMonster != null)
            {
                rangedMonster.GetDamage(damage);
                if (level == 4)
                {
                    rangedMonster.moveSpeed *= 0.5f;
                }
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
            UIManager.Instance.SetText(2, "주변의 적을 느리게 합니다.");
        }
        else if (level == 4)
        {
            return;

        }
    }
}

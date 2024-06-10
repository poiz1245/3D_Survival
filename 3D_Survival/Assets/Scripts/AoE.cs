using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
        this.damage = GameManager.Instance.player.playerAttackPower * 0.3f + damage;
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
        InvokeRepeating("Attack", coolTime, coolTime);
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
            if (rangedMonster != null)
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
            range *= 1.25f;
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
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingLauncher : Weapon
{
    public float fireDelay;

    float spawnTime;
    bool findTarget;
    bool isSpawn = false;
    LayerMask targetLayer;

    public delegate void NearestTargetChanged(GameObject nearestTarget);
    public event NearestTargetChanged OnNearestTargetChanged;

    public GameObject changedTarget
    {
        get { return nearestTargetObject; }
        set
        {
            if (nearestTargetObject != value)
            {
                nearestTargetObject = value;
                OnNearestTargetChanged?.Invoke(nearestTargetObject);
            }
        }
    }
    public GameObject nearestTargetObject { get; private set; }
    public Transform nearestTargetPos { get; private set; }
    public HomingLauncher(int level, float speed, float damage, float range, float fireDelay) : base(level, speed, damage, range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = damage;
        this.range = range;
        this.fireDelay = fireDelay;
    }
    private void Start()
    {
        targetLayer = LayerMask.GetMask("Monster");
        findTarget = false;
    }
    private void Update()
    {
        spawnTime -= Time.deltaTime;

        if (isSpawn)
        {
            ScanTargets();
            if (spawnTime <= 0 && findTarget)
            {
                HomingMissileSpawn(0);
                spawnTime = fireDelay;
            }
        }

        if (changedTarget != null)
        {
            MeleeMonster meleeMonster = changedTarget.GetComponent<MeleeMonster>();
            RangedMonster rangedMonster = changedTarget.GetComponent<RangedMonster>();
            BossMonster bossMonster = changedTarget.GetComponent<BossMonster>();

            if (meleeMonster != null && meleeMonster.hp <= 0)
            {
                changedTarget = null;
            }
            if (rangedMonster != null && rangedMonster.hp <= 0)
            {
                changedTarget = null;
            }
            if (bossMonster != null && bossMonster.hp <= 0)
            {
                changedTarget = null;
            }
        }
    }
    void ScanTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);

        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTargetPos = null;
            GameObject closestTargetObject = null;

            foreach (Collider target in targets)
            {
                MeleeMonster meleeMonster = target.GetComponent<MeleeMonster>();
                if (meleeMonster != null)
                {
                    float distance = Vector3.Distance(transform.position, meleeMonster.transform.position);
                    if (distance < closestDistance && meleeMonster.hp > 0)
                    {
                        closestDistance = distance;
                        closestTargetPos = meleeMonster.transform;
                        closestTargetObject = meleeMonster.gameObject;
                    }
                }

                RangedMonster rangedMonster = target.GetComponent<RangedMonster>();
                if (rangedMonster != null)
                {
                    float distance = Vector3.Distance(transform.position, rangedMonster.transform.position);
                    if (distance < closestDistance && rangedMonster.hp > 0)
                    {
                        closestDistance = distance;
                        closestTargetPos = rangedMonster.transform;
                        closestTargetObject = rangedMonster.gameObject;
                    }
                }

                BossMonster bossMonster= target.GetComponent<BossMonster>();
                if (bossMonster != null)
                {
                    float distance = Vector3.Distance(transform.position, bossMonster.transform.position);
                    if (distance < closestDistance && bossMonster.hp > 0)
                    {
                        closestDistance = distance;
                        closestTargetPos = bossMonster.transform;
                        closestTargetObject = bossMonster.gameObject;
                    }
                }
            }

            nearestTargetPos = closestTargetPos;
            changedTarget = closestTargetObject;
            findTarget = true;
        }
        else
        {
            nearestTargetPos = null;
            findTarget = false;
        }
    }
    public void HomingMissileSpawn(int index)
    {
        HomingMissile homingMissile = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<HomingMissile>();
        homingMissile.SetStatus(damage, speed);
        homingMissile.transform.position = transform.position;
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        print(gameObject.name + "무기 업그레이드 완료 Level : " + level);

        if (level == 1)
        {
            isSpawn = true;
            UIManager.Instance.SetText(1, "적을 추적하는 범위가 증가합니다.");
        }
        else if (level == 2)
        {
            range *= 1.5f;
            UIManager.Instance.SetText(1, "더 빠른 속도로 적을 공격합니다.");
        }
        else if (level == 3)
        {
            fireDelay *= 0.5f;
            UIManager.Instance.SetText(1, "더 강하게 적을 공격합니다.");


        }
        else if (level == 4)
        {
            damage *= 1.5f;
        }
    }
}

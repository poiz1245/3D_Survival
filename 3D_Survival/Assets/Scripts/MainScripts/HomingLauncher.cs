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

            if (meleeMonster != null && meleeMonster.hp <= 0)
            {
                changedTarget = null;
            }
            if (rangedMonster != null && rangedMonster.hp <= 0)
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
            //소환
            isSpawn = true;
        }
        else if (level == 2)
        {
            //level2 : 스캔범위 증가
            range *= 1.5f;

        }
        else if (level == 3)
        {
            //level3 : 발사 속도 증가
            fireDelay *= 0.5f;

        }
        else if (level == 4)
        {
            //level4 : 데미지 증가
            damage *= 1.5f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingLauncher : Weapon
{
    [SerializeField] float spawnDelay;

    float spawnTime;
    bool findTarget;
    LayerMask targetLayer;

    public delegate void NearestTargetChanged(Monster nearestTarget);
    public event NearestTargetChanged OnNearestTargetChanged;
    public Monster changedTarget
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
    public Monster nearestTargetObject { get; private set; }
    public Transform nearestTargetPos { get; private set; }
    public HomingLauncher(int level, float speed, float damage, float range) : base(level, speed, damage, range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = damage;
        this.range = range;

    }
    private void Start()
    {
        targetLayer = LayerMask.GetMask("Monster");
        findTarget = false;
    }
    private void Update()
    {
        spawnTime -= Time.fixedDeltaTime;

        ScanTargets();

        print(findTarget);
        if (spawnTime <= 0 && findTarget)
        {
            HomingMissileSpawn(0);
            spawnTime = spawnDelay;
        }


        if (changedTarget != null)
        {
            if (changedTarget.hp <= 0)
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
            Monster closestTargetObject = null;

            foreach (Collider target in targets)
            {
                Monster monster = target.GetComponent<Monster>();
                if (monster != null)
                {
                    float distance = Vector3.Distance(transform.position, monster.transform.position);
                    if (distance < closestDistance && monster.hp > 0)
                    {
                        closestDistance = distance;
                        closestTargetPos = monster.transform;
                        closestTargetObject = monster;
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
        //레벨 별 업그레이드 구현
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] float spawnDelay;
    [SerializeField] Transform[] spawnPoint;

    float spawnTime;

    public Gun(int level, float speed, float damage, float range, float spawnDelay) : base(level, speed, damage, range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = damage;
        this.range = range;
        this.spawnDelay = 0.5f;
    }

    private void Update()
    {
        spawnTime -= Time.fixedDeltaTime;

        if (spawnTime <= 0)
        {
            BulletSpawn(1, level);
            spawnTime = spawnDelay;
        }
    }
    public void SetBullet(int index, int spawnPointIndex) //총알 스폰
    {
        Bullet bullet = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<Bullet>();
        bullet.SetStatus(damage, speed);

        bullet.transform.position = spawnPoint[spawnPointIndex].position;
        bullet.transform.rotation = spawnPoint[spawnPointIndex].rotation;
    }
    private void BulletSpawn(int index, int level)  //카운트 받아서 스폰 포인트 지정
    {
        int bulletCount = GetBulletCount(level);

        for (int spawnPointIndex = 0; spawnPointIndex < bulletCount; spawnPointIndex++)
        {
            SetBullet(index, spawnPointIndex);
        }
    }
    private int GetBulletCount(int level) //레벨 별로 카운트 설정
    {
        switch (level)
        {
            case 0:
            case 1:
                return 1;
            case 2:
            case 3:
                return 2;
            case 4:
                return 4;
            default:
                return 0;
        }
    }
    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        if (level == 1)
        {
            //level1 : 공속 증가
            speed *= 1.5f;
        }
        else if (level == 2)
        {
            //level2 : 발사 방향 추가
            return;
        }
        else if (level == 3)
        {
            //level3 : 데미지 증가
            damage *= 1.5f;
        }
        else if (level == 4)
        {
            //level4 : 발사 방향 추가
            return;
        }
    }
}
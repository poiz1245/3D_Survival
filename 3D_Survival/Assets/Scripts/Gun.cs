using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] float spawnDelay;

    float spawnTime;

    public Gun(int level, float speed, float damage, float range) : base(level, speed, damage, range)
    {
        this.level = level;
        this.speed = speed;
        this.damage = damage;
        this.range = range;
    }
    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;

        if (spawnTime <= 0) 
        {
            BulletSpawn(1);
            spawnTime = spawnDelay;
        }
    }

    public void BulletSpawn(int index)
    {
        Bullet bullet = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<Bullet>();
        bullet.SetStatus(damage, speed);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }

    public override void WeaponUpGrade()
    {
        base.WeaponUpGrade();
        //레벨 별 업그레이드 구현
    }
}
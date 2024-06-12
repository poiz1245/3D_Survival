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
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && Time.timeScale != 0)
        {
            BulletSpawn(1, level);
            spawnTime = spawnDelay;
        }

        transform.rotation = GameManager.Instance.player.transform.rotation;
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
        print(gameObject.name + "무기 업그레이드 완료 Level : " + level);

        if (level == 1)
        {
            //레벨이 0에서 1로 올라갈 때 들어와서 공속 50%증가
            spawnDelay *= 0.5f;
            UIManager.Instance.SetText(0, "다른 방향의 적을 추가로 공격합니다."); //다음 레벨업 할 때 표시될 text 세팅
        }
        else if (level == 2)
        {
            //1에서 2로 올라갈 때 들어옴
            UIManager.Instance.SetText(0, "적을 더 강하게 공격합니다.");
            return;
        }
        else if (level == 3)
        {
            //2에서 3으로 올라갈 때 데미지 증가
            damage *= 1.5f;
            UIManager.Instance.SetText(0, "공격방향이 더 증가합니다.");
        }
        else if (level == 4)
        {
            return;
        }
    }
}
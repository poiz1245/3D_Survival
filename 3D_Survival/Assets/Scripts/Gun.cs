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
    public void SetBullet(int index, int spawnPointIndex) //�Ѿ� ����
    {
        Bullet bullet = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<Bullet>();
        bullet.SetStatus(damage, speed);

        bullet.transform.position = spawnPoint[spawnPointIndex].position;
        bullet.transform.rotation = spawnPoint[spawnPointIndex].rotation;
    }
    private void BulletSpawn(int index, int level)  //ī��Ʈ �޾Ƽ� ���� ����Ʈ ����
    {
        int bulletCount = GetBulletCount(level);

        for (int spawnPointIndex = 0; spawnPointIndex < bulletCount; spawnPointIndex++)
        {
            SetBullet(index, spawnPointIndex);
        }
    }
    private int GetBulletCount(int level) //���� ���� ī��Ʈ ����
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
        print(gameObject.name + "���� ���׷��̵� �Ϸ� Level : " + level);

        if (level == 1)
        {
            //������ 0���� 1�� �ö� �� ���ͼ� ���� 50%����
            spawnDelay *= 0.5f;
            UIManager.Instance.SetText(0, "�ٸ� ������ ���� �߰��� �����մϴ�."); //���� ������ �� �� ǥ�õ� text ����
        }
        else if (level == 2)
        {
            //1���� 2�� �ö� �� ����
            UIManager.Instance.SetText(0, "���� �� ���ϰ� �����մϴ�.");
            return;
        }
        else if (level == 3)
        {
            //2���� 3���� �ö� �� ������ ����
            damage *= 1.5f;
            UIManager.Instance.SetText(0, "���ݹ����� �� �����մϴ�.");
        }
        else if (level == 4)
        {
            return;
        }
    }
}
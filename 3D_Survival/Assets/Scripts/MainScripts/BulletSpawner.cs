using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;

    float spawnTime;
    int count = 0;
    bool findTarget;

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;
        findTarget = GameManager.Instance.player.findTarget;

        if (spawnTime <= 0 && findTarget) // ���� ���� ���ļ� Ÿ���� �ȸ¾Ƽ� ��ĵ�� �ȵ�
        {
            BulletSpawn(0);
            spawnTime = spawnDelay;
        }
    }

    public void BulletSpawn(int index)
    {
        GameObject bullet = GameManager.Instance.bulletPool.GetBullet(index);
        bullet.transform.position = transform.position;
    }
}

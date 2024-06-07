using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    float spawnTime;

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;

        if (spawnTime <= 0)
        {
            MonsterBulletSpawn(2);
            spawnTime = spawnDelay;
        }
    }

    public void MonsterBulletSpawn(int index)
    {
        GameObject monsterBullet = GameManager.Instance.bulletPool.GetBullet(index);
        monsterBullet.transform.position = transform.position;
        monsterBullet.transform.rotation = transform.rotation;
    }
}
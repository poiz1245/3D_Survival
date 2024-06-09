using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] float damage;

    float spawnTime;

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
        bullet.SetDamage(damage);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }
}
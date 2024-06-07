using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float spawnDelay;
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
        GameObject missile = GameManager.Instance.bulletPool.GetBullet(index);
        missile.transform.position = transform.position;
        missile.transform.rotation = transform.rotation;
    }
}